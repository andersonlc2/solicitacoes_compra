using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Data
{
    public partial class TelaPrincipal : Form
    {
        private MySqlConnection connection = DBConnection.DB_Connection;

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

        private void atualizaPreçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // AQUI TBM VAI ATUALIZAR
        }
    }
}
