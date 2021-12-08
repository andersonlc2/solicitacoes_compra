﻿using System;
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

        public void Save(Produto produto)
        {
            if (produto.ID == null)
                Insert(produto);
            else
                Update(produto);
        }

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

        public Produto GetProdutoById(long? id)
        {
            var produto = new Produto();

            var command = new SqlCommand(
                "SELECT ID, Descricao, PrecoMedio " +
                "FROM Produto " +
                "WHERE id=@id",
                connection
            );
            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    produto.ID = Convert.ToInt64(reader.GetValue(0));
                    produto.Descricao = Convert.ToString(reader.GetValue(1));
                    produto.PrecoMedio = Convert.ToDouble(reader.GetValue(2));

                }
            }

            connection.Close();

            return produto;
        }

        public void Update(Produto produto)
        {
            var command = new SqlCommand(
                "UPDATE Produto " +
                "SET Descricao=@descricao, PrecoMedio=@precomedio " +
                "WHERE ID=@id",
                connection
            );
            command.Parameters.AddWithValue("@id", produto.ID);
            command.Parameters.AddWithValue("@descricao", produto.Descricao);
            command.Parameters.AddWithValue("@precomedio", produto.PrecoMedio);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Remove(Produto produto)
        {
            var command = new SqlCommand(
                "DELETE FROM Produto " +
                "WHERE ID=@id",
                connection
            );
            command.Parameters.AddWithValue("@id", produto.ID);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
