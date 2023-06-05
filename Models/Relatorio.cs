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
        public string RelatorioId { get; set; }
        public string NomeLocal { get; set; } = default!;
        public string NotaIntrodutoria { get; set; } = default!;
        public string Origem { get; set; } = default!;
        public string Spu { get; set; } = default!;
        public string Vinculacao { get; set; } = default!;
        public string EntidadeOrgao { get; set; } = default!;
        public string FotoUrl { get; set; } = default!;
        public string EquipamentoPublico { get; set; } = default!;
        public string EnderecoLocal { get; set; } = default!;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public string UsuarioId { get; set; }
    }
}