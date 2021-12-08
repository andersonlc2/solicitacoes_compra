using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CadastroProdutoForm().ShowDialog();
        }

        private void novaSolicitacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SolicitacaoForm().ShowDialog();
        }
    }
}
