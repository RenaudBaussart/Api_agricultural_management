using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class Fertilizer
    {
        #region attribut
        private int _fertilizerId;
        private string _fertilizerName;
        #endregion
        #region getter setter
        public int FertilizerId { get => _fertilizerId; }
        public string FertilizerName { get => _fertilizerName; }
        #endregion
        #region constructor
        public Fertilizer(int fertilizerId, string fertilizerName)
        {
            _fertilizerId = fertilizerId;
            _fertilizerName = fertilizerName;
        }
        #endregion
        #region methode
        public static List<Fertilizer>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM fertilizer";
            string sqlRequestConnetionString = "server=localhost; user id=root; database=app_gestion_parcelle";
            List<Fertilizer> listOutput = new List<Fertilizer>();
            MySqlConnection sqlRequestConnection = new MySqlConnection(sqlRequestConnetionString);
            try
            {
                sqlRequestConnection.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, sqlRequestConnection);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int fertilizerId = (int)sqlDataReader[0];
                    string fertilizerName = (string)sqlDataReader[1];
                    

                    listOutput.Add(new Fertilizer(fertilizerId,fertilizerName));
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

        #endregion
    }
}
