using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class MateriaControle
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public int CadernoAuditoriaId { get; set; }
        public virtual CadernoAuditoria CadernoAuditoria { get; set; } = default!;
        public virtual List<Questao>? Questao { get; set; }
    }
}