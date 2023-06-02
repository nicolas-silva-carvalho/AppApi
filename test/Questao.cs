using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class Questao
    {
        public int Id { get; set; }
        public int TipoQuestaoId { get; set; }
        public TipoQuestao? TipoQuestao { get; set; }
        public string TextoResposta { get; set; } = default!;
        public int ListaFotos { get; set; }
        public List<Questao> Questaos { get; set; } = default!;
    }
}