using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class Production
    {
        #region attribut
        int _productionid;
        string _productionName,
               _productionUnit;
        #endregion
        #region getter
        public int Productionid { get => _productionid; }
        public string ProductionName { get => _productionName; }
        public string ProductionUnit { get => _productionUnit; }
        #endregion
        #region constructor
        public Production(int productionid, string productionName, string productionUnit)
        {
            _productionid = productionid;
            _productionName = productionName;
            _productionUnit = productionUnit;
        }
        #endregion
        #region methode
        public static List<Production>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM production";
            string sqlRequestConnetionString = "server=localhost; user id=root; database=app_gestion_parcelle";
            List<Production> listOutput = new List<Production>();
            MySqlConnection sqlRequestConnection = new MySqlConnection(sqlRequestConnetionString);
            try
            {
                sqlRequestConnection.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, sqlRequestConnection);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int productionId = (int)sqlDataReader[0];
                    string productionName = (string)sqlDataReader[1],
                           productionUnit = (string)sqlDataReader[2];
                           
                    listOutput.Add(new Production(productionId,productionName,productionUnit));
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
