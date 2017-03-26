using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace oresa.API.Commons
{
    public class DbConnection
    {
        public MySqlConnection OpenConnection() {
            try {
                MySqlConnection scon = new MySqlConnection();

                string connectionString = string.Empty;
                connectionString = ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString;

                scon.ConnectionString = connectionString;
                scon.Open();
                return scon;
            }
            catch (Exception ex)
            {
                throw;
            }
        }       
    }
}