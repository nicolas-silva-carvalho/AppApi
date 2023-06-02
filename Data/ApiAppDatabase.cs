using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAPP.Data
{
    public class ApiAppDatabase
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string RelatoriosCollectionName { get; set; } = null!;
    }
}