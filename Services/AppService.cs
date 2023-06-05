using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Data;
using ApiAPP.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Bson;
using AppApi.Services;

namespace ApiAPP.Services
{
    public class AppService
    {
        private readonly IMongoCollection<Relatorio> _mongoColletion;
        public AppService(IOptions<ApiAppDatabase> _AppApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            _AppApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                _AppApiDatabaseSettings.Value.DatabaseName);

            _mongoColletion = mongoDatabase.GetCollection<Relatorio>(
                _AppApiDatabaseSettings.Value.RelatoriosCollectionName);
        }

        public async Task<List<Relatorio>> PegarRelatoriosTotal() =>
            await _mongoColletion.Find(_ => true).ToListAsync();


        public async Task<Relatorio> PegarRelatorioId(string id) =>
            await _mongoColletion.Find(x => x.Id == id).FirstOrDefaultAsync();


        public async Task CriarRelatorio(Relatorio relatorio) =>
            await _mongoColletion.InsertOneAsync(relatorio);


        public async Task AtualizarRelatorio(string id, Relatorio atualizarRelatorio) =>
            await _mongoColletion.ReplaceOneAsync(x => x.Id == id, atualizarRelatorio);


        public async Task RemoverRelatorioPorId(string id) =>
         await _mongoColletion.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Relatorio>> PegarRelatorioPorIdUsuario(string usuarioId)
        {
            var filter = Builders<Relatorio>.Filter.Eq("UsuarioId", usuarioId);
            var relatorios = _mongoColletion.Find(filter).ToList();
            return relatorios;
        }

        public async Task<Relatorio> PegarUsuarioPorNome(string nome) => 
            await _mongoColletion.Find(x => x.Usuario.Nome == nome).SingleOrDefaultAsync();
          
    

        public async Task<Relatorio> PegarUsuarioPorSenha(string senha)
        {
            var senhaUser = await _mongoColletion.Find(x => x.Usuario.Senha == senha).SingleOrDefaultAsync();
            return senhaUser;
        }
    }
}