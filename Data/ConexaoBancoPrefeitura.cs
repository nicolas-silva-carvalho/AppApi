using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace ApiAPP.Data.Map
{
    public class ConexaoBancoPrefeitura
    {
        public Object conexao()
        {
            var settings = new MongoClientSettings()
            {
                Scheme = ConnectionStringScheme.MongoDB,
                Server = new MongoServerAddress("172.30.101.233", 27017),
                Credential = MongoCredential.CreateCredential("admin", "mongoadmin", "q1w2e3r4$"),
                AllowInsecureTls = true
            };
            return settings;
        }
    

    }
}