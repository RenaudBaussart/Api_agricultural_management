using ApiCreationParcelle.Classes;
using MySql.Data.MySqlClient;

namespace Gestion_parcelle.Classes
{
    internal class Composition
    {
        #region attribut
        int _compositionId,
            _compositionFertilizerId;
        string _composition_element_code_id,
               _composition_unit;


        #endregion
        #region getter
        public int CompositionId { get => _compositionId; }
        public int CompositionFertilizerId { get => _compositionFertilizerId; }
        public string Composition_element_code_id { get => _composition_element_code_id; }
        public string Composition_unit { get => _composition_unit; }
        #endregion
        #region constructor
        public Composition(int compositionId, int compositionFertilizerId, string composition_element_code_id, string composition_unit)
                {
                    _compositionId = compositionId;
                    _compositionFertilizerId = compositionFertilizerId;
                    _composition_element_code_id = composition_element_code_id;
                    _composition_unit = composition_unit;
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
                    int compositionId = (int)sqlDataReader[0],
                        compositionFertilizerId = (int)sqlDataReader[1];
                    string composition_element_code_id = (string)sqlDataReader[2],
                           composition_unit = (string)sqlDataReader[3];



                    listOutput.Add(new Composition(compositionId,compositionFertilizerId,composition_element_code_id,composition_unit));
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
        public static void AddObjectToDB(string elementCode, string elementTag, string unit)
        {
            string sqlRequestCommandString = $"INSERT INTO `chemical_element` (`element_code`, `element_tag`, `unit`) VALUES ('{elementCode}', '{elementTag}', '{unit}');";
            try
            {
                DataBaseConnection.singleton.Close();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, DataBaseConnection.singleton);
                sqlRequestCommand.ExecuteNonQuery();
                DataBaseConnection.singleton.Close();
                Console.WriteLine("Insert was succes full");
            }
            catch (Exception ex)
            {
                DataBaseConnection.singleton.Close();
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
