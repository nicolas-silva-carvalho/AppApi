using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class TipoQuestao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = default!;
        public string Codigo { get; set; } = default!;
        public virtual Questao? Questao { get; set; }
    }
}