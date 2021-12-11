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
            FillTable();
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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnGravar.Enabled = true;
            btnNovo.Enabled = false;

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var auxID = cbxProdutos.SelectedItem.ToString().Split();

            dal.Insert(new Solicitacao()
            {
                Chamado = txtChamado.Text,
                Solicitante = txtSolicitante.Text,
                Departamento = txtDepartamento.Text,
                Produto = dal_Produto.GetProdutoById(Convert.ToInt64(auxID[0])),
                DataChamado = dtpData.Value,
                Quantidade = Convert.ToInt32(txtQuantidade.Text)
            });

            FillTable();
            btnGravar.Enabled = false;
            btnNovo.Enabled = true;
        }

        private void FillTable()
        {
            dgvSolicitacoes.DataSource = dal.GetAllSolic();
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
                FillTable();

        }

    }
}
