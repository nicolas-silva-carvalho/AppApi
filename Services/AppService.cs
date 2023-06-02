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

        public async Task<List<Relatorio>> PegarUsuario() =>
            await _mongoColletion.Find(_ => true).ToListAsync();

    
        public async Task<Relatorio> PegarUsuarioId(string id) =>
            await _mongoColletion.Find(x => x.Id == id).FirstOrDefaultAsync();
        

        public async Task Criar(Relatorio relatorio) => 
            await _mongoColletion.InsertOneAsync(relatorio);
        

        public async Task Atualizar(string id, Relatorio atualizarRelatorio) => 
            await _mongoColletion.ReplaceOneAsync(x => x.Id == id, atualizarRelatorio);
        

        public async Task Remover(string id) =>
         await _mongoColletion.DeleteOneAsync(x => x.Id == id);
        
    }
}