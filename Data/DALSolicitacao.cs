using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Data
{
    public class DALSolicitacao
    {
        private MySqlConnection connection = DBConnection.DB_Connection;

        public void Insert(Solicitacao solic)
        {
            var command = new MySqlCommand(
                "INSERT INTO Solicitacao(Chamado, Solicitante, Departamento, IDProduto, DataChamado, Quantidade) " +
                "Values(@chamado, @solicitante, @departamento, @idproduto, @datachamado, @quantidade)",
                connection
            );
            command.Parameters.AddWithValue("@chamado", solic.Chamado);
            command.Parameters.AddWithValue("@solicitante", solic.Solicitante);
            command.Parameters.AddWithValue("@departamento", solic.Departamento);
            command.Parameters.AddWithValue("@idproduto", solic.Produto.ID);
            command.Parameters.AddWithValue("@datachamado", solic.DataChamado);
            command.Parameters.AddWithValue("@quantidade", solic.Quantidade);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Solicitacao solic)
        {

        }

        public DataTable GetAllSolic()
        {
            var adapter = new MySqlDataAdapter(
                "SELECT Solicitacao.ID as ID, Descricao as Produto, Quantidade as 'Qnt.', PrecoMedio as 'Média MercadoLivre', " +
                "MaiorPreco as 'Maior Preço', MenorPreco as 'Menor Preço', Chamado as 'Número Chamado', DataChamado as Data, Solicitante, Departamento " + 
                "FROM Solicitacao, Produto " +
                "WHERE Produto.ID = Solicitacao.IDProduto"
                , connection
            );
            var builder = new MySqlCommandBuilder(adapter);
            var table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            return table;
        }

        public Solicitacao GetByID(long? id)
        {
            var solicitacao = new Solicitacao();

            return solicitacao;
        }

        public void GetPrecos()
        {   
            /*
            try
            {
                StreamWriter sw = new StreamWriter("D:\\Backup\\Projetos\\C\\C#\\solicitacoes_compra\\Data\\precos.txt");
                var prod = new DataTable();
                prod = GetAllSolic();
                for (var i = 0; i < prod.Rows.Count; i++)
                {
                    var row = prod.Rows[i];
                    sw.WriteLine(row[1]);
                }
                sw.Close();
            }
            catch (Exception ev)
            {
                Console.WriteLine("Exception: " + ev.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            */
            Process myProcessPython = System.Diagnostics.Process.Start("D:\\Backup\\Projetos\\C\\C#\\solicitacoes_compra\\Data\\connectPython.pyw");
            myProcessPython.WaitForExit();
        }

    }
}
