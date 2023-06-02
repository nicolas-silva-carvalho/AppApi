using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ApiAPP.Models
{
    public class Relatorio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("nomeLocal")]
        public string NomeLocal { get; set; } = default!;

        // [BsonElement("notaIntrodutoria")]
        // public string NotaIntrodutoria { get; set; } = default!;
        // [BsonElement("origem")]
        // public string Origem { get; set; } = default!;

        // [BsonElement("Spu")]
        // public string Spu { get; set; } = default!;

        // [BsonElement("vinculacao")]
        // public string Vinculacao { get; set; } = default!;
        // [BsonElement("entidadeOrgao")]
        // public string EntidadeOrgao { get; set; } = default!;

        // [BsonElement("FotoUrl")]
        // public string FotoUrl { get; set; } = default!;

        // [BsonElement("EquipamentoPublico")]
        // public string EquipamentoPublico { get; set; } = default!;

        // [BsonElement("EnderecoLocal")]
        // public string EnderecoLocal { get; set; } = default!;

        // [BsonElement("DataCriacao")]
        // [BsonDateTimeOptions]
        // public DateTime DataCriacao { get; set; }

        // [BsonDateTimeOptions]
        // [BsonElement("DataModificacao")]
        //public DateTime? DataModificacao { get; set; }

        [BsonElement("UsuarioId")]
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}