using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Data;
using ApiAPP.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiAPP.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _mongoColletion;
        public UsuarioService(IOptions<ApiAppDatabase> _AppApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            _AppApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                _AppApiDatabaseSettings.Value.DatabaseName);

            _mongoColletion = mongoDatabase.GetCollection<Usuario>(
                _AppApiDatabaseSettings.Value.UsuarioCollectionName);
        }

        public async Task<Usuario> PegarUsuarioPorNome(string nome) =>
            await _mongoColletion.Find(x => x.Nome == nome).SingleOrDefaultAsync();

        public async Task<Usuario> PegarUsuarioPorSenha(string senha)
        {
            var senhaUser = await _mongoColletion.Find(x => x.Senha == senha).SingleOrDefaultAsync();
            return senhaUser;
        }

        public async Task CriarUsuario(Usuario usuario) =>
            await _mongoColletion.InsertOneAsync(usuario);

        public async Task<List<Usuario>> PegarUsuariosTotal() =>
            await _mongoColletion.Find(_ => true).ToListAsync();
    }
}