using ApiCreationParcelle.Classes;
using MySql.Data.MySqlClient;

namespace Gestion_parcelle.Classes
{
    internal class Composition
    {
        #region attribut
        private int _compositionId;
        private int _compositionFertilizerId;
        private string _compositionElementCodeId;
        private string _compositionUnit;
        #endregion

        #region getter setter
        public int CompositionId { get => _compositionId; }
        public int CompositionFertilizerId { get => _compositionFertilizerId; }
        public string CompositionElementCodeId { get => _compositionElementCodeId; }
        public string CompositionUnit { get => _compositionUnit; }
        #endregion

        #region constructor
        public Composition(int compositionId, int compositionFertilizerId, string compositionElementCodeId, string compositionUnit)
        {
            _compositionId = compositionId;
            _compositionFertilizerId = compositionFertilizerId;
            _compositionElementCodeId = compositionElementCodeId;
            _compositionUnit = compositionUnit;
        }
        #endregion

        #region methode
        public static List<Composition>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM composition";
            List<Composition> listOutput = new List<Composition>();
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listOutput.Add(new Composition(
                        (int)sqlDataReader[0],
                        (int)sqlDataReader[1],
                        (string)sqlDataReader[2],
                        (string)sqlDataReader[3]
                    ));
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

        public static Composition? FetchDataBaseSpecificId(int compositionId)
        {
            string sqlRequestCommandString = $"SELECT * FROM `composition` WHERE composition_id = @compositionId;";
            DataBaseConnection.singleton.Open();
            MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
            sqlRequestCommand.Parameters.AddWithValue("@compositionId", compositionId);
            Composition? specificCompositionOutput = null;
            MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                specificCompositionOutput = new Composition(
                    (int)sqlDataReader[0],
                    (int)sqlDataReader[1],
                    (string)sqlDataReader[2],
                    (string)sqlDataReader[3]
                );
            }
            sqlDataReader.Close();
            DataBaseConnection.singleton.Close();
            return specificCompositionOutput;
        }

        public static void AddObjectToDB(int compositionFertilizerId, string compositionElementCodeId, string compositionUnit)
        {
            string sqlRequestCommandString = "INSERT INTO `composition` (`composition_fertilizer_id`, `composition_element_code_id`, `composition_unit`) VALUES (@compositionFertilizerId, @compositionElementCodeId, @compositionUnit);";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@compositionFertilizerId", compositionFertilizerId);
                sqlRequestCommand.Parameters.AddWithValue("@compositionElementCodeId", compositionElementCodeId);
                sqlRequestCommand.Parameters.AddWithValue("@compositionUnit", compositionUnit);
                sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();
                Console.WriteLine("Insert was successful");
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public static void RemoveFromDB(int compositionId)
        {
            string sqlRequestCommandString = "DELETE FROM `composition` WHERE `composition`.`composition_id` = @compositionId;";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@compositionId", compositionId);
                sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();
                Console.WriteLine("Delete was successful");
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
            }
        }

        public static void UpdateObjectInDB(int compositionId, int compositionFertilizerId, string compositionElementCodeId, string compositionUnit)
        {
            string sqlRequestCommandString = "UPDATE `composition` SET `composition_fertilizer_id` = @compositionFertilizerId, `composition_element_code_id` = @compositionElementCodeId, `composition_unit` = @compositionUnit WHERE `composition_id` = @compositionId;";
            try
            {
                DataBaseConnection.singleton.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.Parameters.AddWithValue("@compositionId", compositionId);
                sqlRequestCommand.Parameters.AddWithValue("@compositionFertilizerId", compositionFertilizerId);
                sqlRequestCommand.Parameters.AddWithValue("@compositionElementCodeId", compositionElementCodeId);
                sqlRequestCommand.Parameters.AddWithValue("@compositionUnit", compositionUnit);
                int rowsAffected = sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Update was successful");
                }
                else
                {
                    Console.WriteLine("No rows were updated. The composition ID might not exist.");
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
