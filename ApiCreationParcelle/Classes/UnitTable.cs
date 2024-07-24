using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class UnitTable
    {
        public static List<string>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM unit";
            string sqlRequestConnetionString = "server=localhost; user id=root; database=app_gestion_parcelle";
            List<string> listOutput = new List<string>();
            MySqlConnection sqlRequestConnection = new MySqlConnection(sqlRequestConnetionString);
            try
            {
                sqlRequestConnection.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, sqlRequestConnection);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    listOutput.Add(sqlDataReader[0].ToString());
                    
                }
                sqlDataReader.Close();
                sqlRequestConnection.Close();
                return listOutput;
            }
            catch (Exception ex)
            {
                sqlRequestConnection.Close();
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
