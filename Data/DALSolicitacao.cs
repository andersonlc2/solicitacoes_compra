using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Forms;

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
            var command = new MySqlCommand(
                "UPDATE Solicitacao SET Chamado=@chamado, Solicitante=@solicitante, Departamento=@dpt, " +
                "DataChamado=@data, Quantidade=@qnt, IDProduto=@produto " +
                "WHERE ID=@id",
                connection
            );
            
            command.Parameters.AddWithValue("@id", solic.ID);
            command.Parameters.AddWithValue("@chamado", solic.Chamado);
            command.Parameters.AddWithValue("@solicitante", solic.Solicitante);
            command.Parameters.AddWithValue("@dpt", solic.Departamento);
            command.Parameters.AddWithValue("@data", solic.DataChamado);
            command.Parameters.AddWithValue("@qnt", solic.Quantidade);
            command.Parameters.AddWithValue("@produto", solic.Produto.ID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal void Save(Solicitacao solicitacao)
        {
            if (solicitacao.ID == null)
            {
                Insert(solicitacao);
            }
            else
            {
                Update(solicitacao);
            }
        }

        public DataTable GetAllSolic()
        {
            var adapter = new MySqlDataAdapter(
                "SELECT Solicitacao.ID as ID, Descricao as Produto, Quantidade as 'Qnt.', PrecoMedio as 'Média MercadoLivre', " +
                "MaiorPreco as 'Maior Preço', MenorPreco as 'Menor Preço', DataChamado as 'Data Chamado', Chamado as 'Número Chamado', Solicitante, Departamento " + 
                "FROM Solicitacao, Produto " +
                "WHERE Produto.ID = Solicitacao.IDProduto "
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
            var dalProduto = new DALProduto();
            var solicitacao = new Solicitacao();
            long IDProduto = -1;
            
            var command = new MySqlCommand(
                "SELECT ID, Chamado, Solicitante, Departamento, IDProduto, DataChamado, Quantidade " +
                "FROM solicitacao " +
                "WHERE ID=@id",
                connection
            );
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    solicitacao.ID = reader.GetInt64(0);
                    solicitacao.Chamado = Convert.ToString(reader["Chamado"]);
                    solicitacao.Solicitante = Convert.ToString(reader["Solicitante"]);
                    solicitacao.Departamento = Convert.ToString(reader["Departamento"]);
                    solicitacao.DataChamado = reader.GetDateTime(5);
                    solicitacao.Quantidade = reader.GetInt32(6);
                    IDProduto = reader.GetInt64(4);
                }
            }
            connection.Close();
            if (IDProduto > 0)
                solicitacao.Produto = dalProduto.GetProdutoById(IDProduto);

            return solicitacao;
        }

        public void GetPrecos()
        {
            using (Process myProcessPython = Process.Start("D:\\Backup\\Projetos\\C\\C#\\solicitacoes_compra\\Data\\connectPython.pyw"))
                myProcessPython.WaitForExit();
        }

        public void Remove(Solicitacao solic)
        {
            var command = new MySqlCommand(
                "DELETE FROM Solicitacao " +
                "WHERE ID=@id",
                connection
            );
            command.Parameters.AddWithValue("@id", solic.ID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
