using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCreationParcelle.Classes
{
    // 
    internal class DataBaseConnection
    {
        private static string sqlRequestConnectionString = "server=localhost;user=root;database=app_gestion_parcelle;";
        public static MySqlConnection singleton = new MySqlConnection(sqlRequestConnectionString);
    }
}
