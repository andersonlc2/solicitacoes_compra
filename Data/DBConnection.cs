using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Data
{
    public sealed class DBConnection
    {
        private static volatile SqlConnection instance;

        private DBConnection() { }

        public static SqlConnection DB_Connection
        {
            get 
            {
                if (instance == null)
                {
                    instance = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["Data"].ConnectionString
                    );
                }
                return instance;
            }
        }
    }
}
