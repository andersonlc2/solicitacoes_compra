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
    public partial class CadastroProdutoForm : Form
    {
        private DALProduto dal = new DALProduto();
        public CadastroProdutoForm()
        {
            InitializeComponent();
            FilldgvProdutos();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            dal.Insert(new Produto()
            {
                Descricao = txtDescricao.Text,
                PrecoMedio = Convert.ToDouble(txtPreco.Text)
            });

            MessageBox.Show("Produto gravado com sucesso.", "System Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearControls();

            txtPreco.Enabled = false;
            txtDescricao.Enabled = false;
            btnEditar.Enabled = true;
            btnRemover.Enabled = true;
            btnNovo.Enabled = true;
            btnGravar.Enabled = false;
        }

        private void ClearControls()
        {
            txtID.Text = String.Empty;
            txtDescricao.Text = String.Empty;
            txtPreco.Text = String.Empty;
            FilldgvProdutos();
            dgvProdutos.ClearSelection();
        }

        private void FilldgvProdutos()
        {
            dgvProdutos.DataSource = dal.GetAllProdutos();

            dgvProdutos.Columns[0].Width = 60;
            dgvProdutos.Columns[1].Width = 252;
            dgvProdutos.Columns[2].Width = 120;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtPreco.Enabled = true;
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
            btnEditar.Enabled = false;
            btnRemover.Enabled = false;
            btnNovo.Enabled = false;
            btnGravar.Enabled = true;
            
        }
    }
}
