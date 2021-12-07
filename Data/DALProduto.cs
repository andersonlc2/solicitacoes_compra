using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Data
{
    public class DALProduto
    {
        private SqlConnection connection = DBConnection.DB_Connection;

        public void Insert(Produto produto)
        {
            var command = new SqlCommand(
                "INSERT INTO Produto(Descricao, PrecoMedio) " +
                "Values(@descricao, @precomedio)", 
                connection
            );
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@precomedio", produto.PrecoMedio);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public DataTable GetAllProdutos()
        {
            var adapter = new SqlDataAdapter(
                "SELECT ID, Descricao, PrecoMedio " +
                "FROM Produto",
                connection
            );
            var builder = new SqlCommandBuilder(adapter);
            var table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        public void Update(Produto produto)
        {

        }
    }
}
