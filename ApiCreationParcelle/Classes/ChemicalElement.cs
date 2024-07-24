using ApiCreationParcelle.Classes;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using static Org.BouncyCastle.Math.Primes;

namespace Gestion_parcelle.Classes
{
    internal class ChemicalElement
    {
        #region attribut
        private string _elementCode;
        private string _elementTag;
        private string _elementUnit;
        #endregion
        #region getter setter
        public string ElementCode { get => _elementCode; }
        public string ElementTag { get => _elementTag; }
        public string ElementUnit { get => _elementUnit; }
        #endregion
        #region constructor
        public ChemicalElement(string elementCode, string elementTag, string elementUnit)
        {
            _elementCode = elementCode;
            _elementTag = elementTag;
            _elementUnit = elementUnit;
            
        }
        #endregion
        #region methode
        public static List<ChemicalElement>? FetchDataBaseList() 
        {
            string sqlRequestCommandString = "SELECT * FROM chemical_element";
            List<ChemicalElement> listOutput = new List<ChemicalElement>();
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listOutput.Add(new ChemicalElement((string)sqlDataReader[0], (string)sqlDataReader[1], (string)sqlDataReader[2]));
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
            finally { DataBaseConnection.singleton.Close(); }
        }
        public static ChemicalElement? FetchDataBaseSpecificId(string elementCode) 
        {
            string sqlRequestCommandString = $"SELECT * FROM `chemical_element` WHERE element_code = '{elementCode}';";
            DataBaseConnection.singleton.Open();
            MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
            ChemicalElement? specificElementOutput = null;
            MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                string elementCodeFetched = (string)sqlDataReader[0],
                       elementTagFetched = (string)sqlDataReader[1],
                       elementUnitFetched = (string)sqlDataReader[2];
                specificElementOutput = new ChemicalElement(elementCode, elementTagFetched, elementUnitFetched);
            }
            sqlDataReader.Close();
            DataBaseConnection.singleton.Close();
            return specificElementOutput;
        }
        public static void AddObjectToDB(string elementCode, string elementTag,string unit)
        {
            string sqlRequestCommandString = $"INSERT INTO `chemical_element` (`element_code`, `element_tag`, `unit`) VALUES (@elementCode, @elementTag, @unit);";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@elementCode", elementCode);
                sqlRequestCommand.Parameters.AddWithValue("@elementTag", elementTag);
                sqlRequestCommand.Parameters.AddWithValue("@unit", unit);
                sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();
                Console.WriteLine("Insert was succesfull");
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
            }
        }
        public static void RemoveFromDB(string elementCode)
        {
            string sqlRequestCommandString = $"DELETE FROM `chemical_element` WHERE `chemical_element`.`element_code` =@elementCode;";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@elementCode", elementCode);
                sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();
                Console.WriteLine("Delete was succesfull");
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
            }
        }
        public static void UpdateObjectInDB(string elementCode, string elementTag, string unit)
        {
            string sqlRequestCommandString = "UPDATE `chemical_element` SET `element_tag` = @elementTag, `unit` = @unit WHERE `element_code` = @elementCode;";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@elementCode", elementCode);
                sqlRequestCommand.Parameters.AddWithValue("@elementTag", elementTag);
                sqlRequestCommand.Parameters.AddWithValue("@unit", unit);
                int rowsAffected = sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update was successful");
                }
                else
                {
                    Console.WriteLine("No rows were updated. The element code might not exist.");
                }
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine($"An error occurred during update: {ex.Message}");
            }
        }
        #endregion
    }
}
