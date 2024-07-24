using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class Spraying
    {
        #region attribut
        private int _sprayingId,
                    _sprayingQuantity,
                    _sprayingPlotId,
                    _sprayinFertilizerId;
        private string _sprayingDate;
        #endregion
        #region getter
        public int SprayingId { get => _sprayingId; }
        public int SprayingQuantity { get => _sprayingQuantity; }
        public int SprayingPlotId { get => _sprayingPlotId; }
        public int SprayinFertilizerId { get => _sprayinFertilizerId; }
        public string SprayingDate { get => _sprayingDate; }
        #endregion
        #region constructor
        public Spraying(int sprayingId, int sprayingQuantity, int sprayingPlotId, int sprayinFertilizerId, string sprayingDate)
        {
            _sprayingId = sprayingId;
            _sprayingQuantity = sprayingQuantity;
            _sprayingPlotId = sprayingPlotId;
            _sprayinFertilizerId = sprayinFertilizerId;
            _sprayingDate = sprayingDate;
        }
        #endregion
        #region methode
        public static List<Spraying>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM spraying";
            string sqlRequestConnetionString = "server=localhost; user id=root; database=app_gestion_parcelle";
            List<Spraying> listOutput = new List<Spraying>();
            MySqlConnection sqlRequestConnection = new MySqlConnection(sqlRequestConnetionString);
            try
            {
                sqlRequestConnection.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, sqlRequestConnection);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int sprayingId = (int)sqlDataReader[0],
                        sprayingQuantity = (int)sqlDataReader[1],
                        sprayingPlotId = (int)sqlDataReader[2],
                        sprayinFertilizerId = (int)sqlDataReader[3];
                    string sprayingDate = sqlDataReader[4].ToString();
                    listOutput.Add(new Spraying(sprayingId,sprayingQuantity,sprayingPlotId,sprayinFertilizerId,sprayingDate));
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
