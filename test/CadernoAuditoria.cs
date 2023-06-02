using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class CadernoAuditoria
    {
        public int Id { get; set; }
        public int AnoCaderno { get; set; }
        public List<MateriaControle> MateriaControle { get; set; } = default!;
        
    }
}