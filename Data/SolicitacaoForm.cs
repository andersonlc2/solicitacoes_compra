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
    public partial class SolicitacaoForm : Form
    {
        public DALProduto dal_Produto = new DALProduto();
        public DALSolicitacao dal = new DALSolicitacao();
        public Solicitacao solicitacaoAtual;

        public SolicitacaoForm()
        {
            InitializeComponent();
            InitializeCbx();
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


            btnGravar.Enabled = false;
        }
    }
}
