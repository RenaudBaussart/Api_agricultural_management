using ApiCreationParcelle.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class Culture
    {
        #region attribut
        private int _cultureId;
        private string _cultureStartingDate;
        private string _cultureEndingDate;
        private string _cultureQuantityGathered;
        private int _cultureProductionCode;
        #endregion
        #region getter setter
        public int CultureId { get => _cultureId; set => _cultureId = value; }
        public string CultureStartingDate { get => _cultureStartingDate; set => _cultureStartingDate = value; }
        public string CultureEndingDate { get => _cultureEndingDate; set => _cultureEndingDate = value; }
        public string CultureQuantityGathered { get => _cultureQuantityGathered; set => _cultureQuantityGathered = value; }
        public int CultureProductionCode { get => _cultureProductionCode; set => _cultureProductionCode = value; }
        #endregion
        #region constructor
        public Culture(int cultureId, string cultureStartingDate, string cultureEndingDate, string cultureQuantityGathered, int cultureProductionCode)
        {
            _cultureId = cultureId;
            _cultureStartingDate = cultureStartingDate;
            _cultureEndingDate = cultureEndingDate;
            _cultureQuantityGathered = cultureQuantityGathered;
            _cultureProductionCode = cultureProductionCode;
        }
        #endregion
        #region methode
        public static List<Culture>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM culture";
            List<Culture> listOutput = new List<Culture>();
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int fetchedCultureId = (int)sqlDataReader[0];
                    string fetchedCultureStartingDate = sqlDataReader[1].ToString();
                    string fetchedCultureEndingDate = sqlDataReader[2].ToString();
                    string fetchedCultureQuantityGathered = (string)sqlDataReader[3];
                    int fetchedCultureProductionCode = (int)sqlDataReader[4];

                listOutput.Add(new Culture(fetchedCultureId,fetchedCultureStartingDate,fetchedCultureEndingDate,fetchedCultureQuantityGathered,fetchedCultureProductionCode));
                }
                sqlDataReader.Close();
                DataBaseConnection.singleton.Close();
                return listOutput;
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
