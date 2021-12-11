using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Data
{
    public sealed class DBConnection
    {
        private static volatile MySqlConnection instance;

        private DBConnection() { }

        public static MySqlConnection DB_Connection
        {
            get 
            {
                if (instance == null)
                {
                    instance = new MySqlConnection(
                        ConfigurationManager.ConnectionStrings["dbsolicit"].ConnectionString
                    );
                }
                return instance;
            }
        }
    }
}
