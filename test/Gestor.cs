using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class Gestor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Telefone { get; set; } = default!;
        public string Matricula { get; set; } = default!;
    }
}