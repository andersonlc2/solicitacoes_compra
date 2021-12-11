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
        private Produto produtoAtual;
        public CadastroProdutoForm()
        {
            InitializeComponent();
            ClearControls();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            dal.Save(new Produto()
            {
                ID = string.IsNullOrEmpty(txtID.Text) ? (long?) null: Convert.ToInt64(txtID.Text),
                Descricao = txtDescricao.Text,
                PrecoMedio = string.IsNullOrEmpty(txtPreco.Text) ? (double?) 0 : Convert.ToDouble(txtPreco.Text)
            });
            ClearControls();
            MessageBox.Show("Produto gravado com sucesso.", "System Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtPreco.Enabled = false;
            txtDescricao.Enabled = false;
            btnEditar.Enabled = false;
            btnRemover.Enabled = false;
            btnNovo.Enabled = true;
            btnNovo.Focus();
            btnGravar.Enabled = false;
            dgvProdutos.Enabled = true;

        }

        private void ClearControls()
        {
            txtID.Text = String.Empty;
            txtDescricao.Text = String.Empty;
            txtPreco.Text = String.Empty;
            FilldgvProdutos();
            dgvProdutos.ClearSelection();
            btnNovo.Focus();
        }

        private void FilldgvProdutos()
        {
            dgvProdutos.DataSource = dal.GetAllProdutos();

            dgvProdutos.Columns[0].Width = 60;
            dgvProdutos.Columns[1].Width = 295;
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
            dgvProdutos.Enabled = false;
            
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            this.produtoAtual = dal.GetProdutoById(
                Convert.ToInt64(dgvProdutos.Rows[e.RowIndex].Cells[0].Value)
            );
            btnEditar.Enabled = true;
            btnRemover.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtID.Text = this.produtoAtual.ID.ToString();
            txtDescricao.Text = this.produtoAtual.Descricao;
            txtPreco.Text = this.produtoAtual.PrecoMedio.ToString();

            txtPreco.Enabled = true;
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
            btnEditar.Enabled = false;
            btnRemover.Enabled = false;
            btnNovo.Enabled = false;
            btnGravar.Enabled = true;
            dgvProdutos.Enabled = false;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Confirma exclusão do produto?", "System Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dal.Remove(this.produtoAtual);
                ClearControls();
                MessageBox.Show("Produto removido com sucesso.", "System Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnEditar.Enabled=false;
            btnRemover.Enabled=false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearControls();

            txtDescricao.Enabled=false;
            txtPreco.Enabled=false;
            btnGravar.Enabled = false;
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnRemover.Enabled = false;
            dgvProdutos.Enabled = true;
        }
    }
}
