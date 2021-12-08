using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Data
{
    public class DALSolicitacao
    {
        private SqlConnection connection = DBConnection.DB_Connection;

        public void Insert(Solicitacao solic)
        {
            var command = new SqlCommand(
                "INSERT INTO Solicitacao(Chamado, Solicitante, Departamento, IDProduto, Data, Quantidade) " +
                "Values(@chamado, @solicitante, @departamento, @idproduto, @data, @quantidade)",
                connection
            );
            command.Parameters.AddWithValue("@chamado", solic.Chamado);
            command.Parameters.AddWithValue("@solicitante", solic.Solicitante);
            command.Parameters.AddWithValue("@departamento", solic.Departamento);
            command.Parameters.AddWithValue("@idproduto", solic.Produto.ID);
            command.Parameters.AddWithValue("@data", solic.DataChamado);
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
            var adapter = new SqlDataAdapter(
                "SELECT " +  //////////////////////// PAREI AQUI
                "FROM " +
                "WHERE "
                , connection
            );
            var table = new DataTable();

            return table;
        }

        public Solicitacao GetByID(long? id)
        {
            var solicitacao = new Solicitacao();

            return solicitacao;
        }

    }
}
