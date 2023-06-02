using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ApiAPP.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("Nome")]
        public string Nome { get; set; } = default!;

        // [BsonElement("Email")]
        // public string Email { get; set; } = default!;

        // [BsonElement("Telefone")]
        // public string Telefone { get; set; } = default!;

        // public string FotoUrl { get; set; } = default!;
        //public IEnumerable<Relatorio> Relatorios { get; set; } = default!;
    }
}