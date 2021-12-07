using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Produto
    {
        public long? ID { get; set; }
        public string Descricao { get; set; }
        public double? PrecoMedio { get; set; }

        public Produto ()
        {
            this.ID = null;
            this.PrecoMedio = 0;
        }

    }
}
