using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.test
{
    public class Foto
    {
        public int Id { get; set; }
        public string UrlLocal { get; set; } = default!;
        public string UrlNav { get; set; } = default!;
    }
}