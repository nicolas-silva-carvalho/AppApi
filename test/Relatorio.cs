using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class Relatorio2
    {
        public int Id { get; set; }
        public string NomeLocal { get; set; } = default!;
        public string Spu { get; set; } = default!;
        public int OrigemRelatorioId { get; set; }
        public virtual OrigemRelatorio? OrigemRelatorio { get; set; }
        public string Vinculacao { get; set; } = default!;
        public int? OrgaoEntidadeId { get; set; }
        public virtual OrgaoEntidade? OrgaoEntidade { get; set; }
        public string FotoUrl { get; set; } = default!;
        public string EquipamentoPublico { get; set; } = default!;
        public string EnderecoLocal { get; set; } = default!;
        public int GestorId { get; set; }
        public virtual Gestor? Gestor { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
        public int? UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public int CadernoAuditoriaId { get; set; }
        public virtual CadernoAuditoria? CadernoAuditoria { get; set; }
        public List<QuestaoResposta> QuestaoResposta { get; set; } = default!;

    }
}