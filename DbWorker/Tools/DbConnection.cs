using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DbWorker.Tools
{
    class DbConnection
    {
        public static MySqlConnection GetConnection()
        {
            string connectionString = "server=127.0.0.1;database=my_online_shop;user=root;password=123;port=50000;charset=utf8";
            return new MySqlConnection(connectionString);
        }
    }
}   