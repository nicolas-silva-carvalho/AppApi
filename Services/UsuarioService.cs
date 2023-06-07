using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Data;
using ApiAPP.Data.Map;
using ApiAPP.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiAPP.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _mongoColletion;
        private readonly ConexaoBancoPrefeitura _conexaoBancoPrefeitura;
        public UsuarioService(IOptions<ApiAppDatabase> _AppApiDatabaseSettings, ConexaoBancoPrefeitura conexaoBancoPrefeitura)
        {
            _conexaoBancoPrefeitura = conexaoBancoPrefeitura;
            var mongoClient = new MongoClient((MongoClientSettings)_conexaoBancoPrefeitura.conexao());
            var mongoDatabase = mongoClient.GetDatabase("Relatorio");
            _mongoColletion = mongoDatabase.GetCollection<Usuario>("Usuarios");
        }

        public async Task<Usuario> PegarUsuarioPorNomeESenha(string nome, string senha) =>
            await _mongoColletion.Find(x => x.Nome.ToUpper() == nome.ToUpper() && x.Senha == senha).SingleOrDefaultAsync();

        public async Task CriarUsuario(Usuario usuario) =>
            await _mongoColletion.InsertOneAsync(usuario);

        public async Task<List<Usuario>> PegarUsuariosTotal() =>
            await _mongoColletion.Find(_ => true).ToListAsync();
    }
}