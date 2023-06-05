using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ApiAPP.Models
{
    public class Usuario
    {
        [BsonId]
        public string UsuarioId { get; set; }
        public string Nome { get; set; } = default!;
        public string Senha { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Telefone { get; set; } = default!;
        public string FotoUrl { get; set; } = default!;
        public string Token { get; set; } = default!;
        
    }
}