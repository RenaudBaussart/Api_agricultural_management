using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_parcelle.Classes
{
    internal class Plot
    {
        #region attribut
        int _plotId,
            _plotSurface,
            _plotCultureId;
        string _plotName,
               _plotCordinate;



        #endregion
        #region getter setter
        public int PlotId { get => _plotId; }
        public int PlotSurface { get => _plotSurface; }
        public int PlotCultureId { get => _plotCultureId; }
        public string PlotName { get => _plotName; }
        public string PlotCordinate { get => _plotCordinate; }
        #endregion
        #region constructor
        public Plot(int plotId, int plotSurface, int plotCultureId, string plotName, string plotCordinate)
        {
            _plotId = plotId;
            _plotSurface = plotSurface;
            _plotCultureId = plotCultureId;
            _plotName = plotName;
            _plotCordinate = plotCordinate;
        }


        #endregion
        #region methode
        public static List<Plot>? FetchDataBaseList()
        {
            string sqlRequestCommandString = "SELECT * FROM plot";
            string sqlRequestConnetionString = "server=localhost; user id=root; database=app_gestion_parcelle";
            List<Plot> listOutput = new List<Plot>();
            MySqlConnection sqlRequestConnection = new MySqlConnection(sqlRequestConnetionString);
            try
            {
                sqlRequestConnection.Open();
                MySqlCommand sqlRequestCommand = new MySqlCommand(sqlRequestCommandString, sqlRequestConnection);
                MySqlDataReader sqlDataReader = sqlRequestCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    int plotId = (int)sqlDataReader[0],
                        plotSurface = (int)sqlDataReader[1],
                        plotCultureId = (int)sqlDataReader[2];
                    string plotName = (string)sqlDataReader[3],
                           plotCordinate = (string)sqlDataReader[4];
                    listOutput.Add(new Plot(plotId,plotSurface,plotCultureId,plotName,plotCordinate));
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
