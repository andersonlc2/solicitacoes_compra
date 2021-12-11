using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data
{
    public partial class SolicitacaoForm : Form
    {
        private Thread thread;

        public DALProduto dal_Produto = new DALProduto();
        public DALSolicitacao dal = new DALSolicitacao();
        public Solicitacao solicitacaoAtual;

        public SolicitacaoForm()
        {
            InitializeComponent();
            InitializeCbx();
            ClearControls();
        }

        private void InitializeCbx()
        {
            cbxProdutos.Items.Clear();

            foreach (Produto produto in dal_Produto.GetIList())
            {
                var exib = produto.ID + " - " + produto.Descricao;
                cbxProdutos.Items.Add(exib);
            }
        }

        private void ClearControls()
        {
            txtID.Text = string.Empty;
            txtChamado.Text = string.Empty;
            txtChamado.Focus();
            txtSolicitante.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            cbxProdutos.SelectedIndex = -1;
            dtpData.Value = DateTime.Now;

            txtChamado.Enabled = false;
            txtSolicitante.Enabled = false;
            txtDepartamento.Enabled = false;
            txtQuantidade.Enabled = false;
            cbxProdutos.Enabled = false;
            dtpData.Enabled = false;

            FillTable();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ClearControls();
            txtChamado.Enabled = true;
            txtSolicitante.Enabled = true;
            txtDepartamento.Enabled = true;
            txtQuantidade.Enabled = true;
            cbxProdutos.Enabled = true;
            dtpData.Enabled = true;
            btnGravar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnRemover.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var auxID = cbxProdutos.SelectedItem.ToString().Split();

            dal.Save(new Solicitacao()
            {
                ID = string.IsNullOrEmpty(txtID.Text) ? (long?)null : Convert.ToInt64(txtID.Text),
                Chamado = txtChamado.Text,
                Solicitante = txtSolicitante.Text,
                Departamento = txtDepartamento.Text,
                Produto = dal_Produto.GetProdutoById(Convert.ToInt64(auxID[0])),
                DataChamado = dtpData.Value,
                Quantidade = Convert.ToInt32(txtQuantidade.Text)
            });

            ClearControls();
            MessageBox.Show("Solicitacao gravada com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnGravar.Enabled = false;
            btnAtualizar.Enabled = true;
            btnCancelar.Enabled = false;
            btnNovo.Enabled = true;
            dgvSolicitacoes.Enabled = true;
        }

        private void FillTable()
        {
            dgvSolicitacoes.DataSource = dal.GetAllSolic();

            dgvSolicitacoes.Columns[0].Width = 50;
            dgvSolicitacoes.Columns[1].Width = 250;
            dgvSolicitacoes.Columns[2].Width = 40;

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            //Cria uma nova thread, indicando qual método essa thread deverá executar
            thread = new Thread(() => {
                dal.GetPrecos();
                //Código que será executado em paralelo ao resto do código
            });
            //Inicia a execução da thread (em paralelo a esse código)
            thread.Start();


            progressBar1.Maximum = dal_Produto.GetIList().Count();
            progressBar1.Visible = true;

            for (int i = 0; i < progressBar1.Maximum; i++)
            {
                progressBar1.Value++;
                Thread.Sleep(1000);
            }

            var res = MessageBox.Show("Preços atualizados com sucesso!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
                progressBar1.Visible = false;
                progressBar1.Value = 0;

                ClearControls();
                btnGravar.Enabled = false;
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnAtualizar.Enabled = true;
                btnRemover.Enabled = false;
                btnCancelar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ClearControls();

            btnGravar.Enabled = false;
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            btnAtualizar.Enabled = true;
            btnRemover.Enabled = false;
            btnCancelar.Enabled = true;
            dgvSolicitacoes.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtID.Text = solicitacaoAtual.ID.ToString();
            txtChamado.Text = solicitacaoAtual.Chamado;
            txtSolicitante.Text = solicitacaoAtual.Solicitante;
            txtDepartamento.Text = solicitacaoAtual.Departamento;
            txtQuantidade.Text = solicitacaoAtual.Quantidade.ToString();
            // Formata uma string com o padrao do combo box
            var aux = solicitacaoAtual.Produto.ID + " - " + solicitacaoAtual.Produto.Descricao;
            // Passa para o item selecionado o retorno da busca pela string que criei
            cbxProdutos.SelectedIndex = cbxProdutos.FindString(aux); ;
            dtpData.Value = DateTime.Now;

            txtChamado.Enabled = true;
            txtSolicitante.Enabled = true;
            txtDepartamento.Enabled = true;
            txtQuantidade.Enabled = true;
            cbxProdutos.Enabled = true;
            dtpData.Enabled = true;
            btnGravar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnRemover.Enabled = false;
            btnCancelar.Enabled = true;
            dgvSolicitacoes.Enabled = false;
        }

        private void dgvSolicitacoes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            this.solicitacaoAtual = dal.GetByID(
                Convert.ToInt64(dgvSolicitacoes.Rows[e.RowIndex].Cells[0].Value)
            );

            btnEditar.Enabled = true;
            btnRemover.Enabled = true;
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Confirma exclusão da solicitacao?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dal.Remove(this.solicitacaoAtual);
                ClearControls();
                MessageBox.Show("Solicitacao removida com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnEditar.Enabled = false;
            btnRemover.Enabled = false;
        }
    }
}
