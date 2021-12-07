using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Solicitacao
    {
        public long? ID { get; set; }
        public string Chamado { get; set; }
        public string Solicitante { get; set; }
        public string Departamento { get; set; }
        public Produto Produto { get; set; }

        public Solicitacao ()
        {
            this.ID = null;
        }
    }

}
