using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace JSONAPI
{
    /*
    Når ReadJSON.aspx lader, så henter ReadJSON.aspx.cs informasjon fra API og lagrer den i instansierte collections for å gjøre
    informasjonen mulig å aksessere og behandle. Når den har hentet alt av informasjon den trenger fra API så 
    kobler den seg opp mot MySQL databasen og laster opp informasjonen.
    */
    public partial class ReadJSON : System.Web.UI.Page
    {
        StringBuilder driversTable = new StringBuilder();
        StringBuilder constructorsTable = new StringBuilder();
        StringBuilder racesTable = new StringBuilder();
        StringBuilder circuitsTable = new StringBuilder();
        StringBuilder resultsTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                //henter informasjonen for samlingene gjennom webklienten og instansierer
                MySqlCommand cmd = EstablishConnectionToDB();
                DriversCollection driversCollection = GetDriversFromAPI(webClient);
                ConstructorsCollection constructorsCollection = GetConstructorsFromAPI(webClient);
                CircuitsCollection circuitsCollection = GetCircuitsFromAPI(webClient);
                RacesCollection racesCollection = GetRacesFromAPI(webClient);

                //viser det den har hentet fra API i tabeller
                DisplayTables(driversCollection, constructorsCollection, racesCollection, circuitsCollection);

                //kobler til database og setter inn API samlingene      
                UpdateDB(driversCollection, constructorsCollection, racesCollection, circuitsCollection, cmd);
            }
        }

        private void DisplayTables(DriversCollection driversCollection, ConstructorsCollection constructorsCollection, RacesCollection racesCollection, CircuitsCollection circuitsCollection)
        {
            if (!Page.IsPostBack)
            {
                //DRIVERS
                driversTable.Append("<h1>DRIVERS</h1>");
                driversTable.Append("<table border ='1'>");
                driversTable.Append("<tr><th>Driver ID</th><th>url</th><th>Given name</th><th>Family name</th><th>Date of Birth</th><th>Nationality</th>");
                driversTable.Append("</tr>");

                for (var i = 0; i < driversCollection.Drivers.Count; i++)
                {
                    driversTable.Append("<tr>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].DriverID + "</td>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].Url + "</td>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].GivenName + "</td>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].FamilyName + "</td>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].DateOfBirth + "</td>");
                    driversTable.Append("<td>" + driversCollection.Drivers[i].Nationality + "</td>");
                    driversTable.Append("</tr>");
                }
                driversTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = driversTable.ToString() });

                //CONSTRUCTORS
                constructorsTable.Append("<h1>CONSTRUCTORS</h1>");
                constructorsTable.Append("<table border ='1'>");
                constructorsTable.Append("<tr><th>Constructor ID</th><th>url</th><th>Name</th><th>Nationality</th>");
                constructorsTable.Append("</tr>");

                for (var i = 0; i < constructorsCollection.Constructors.Count; i++)
                {
                    constructorsTable.Append("<tr>");
                    constructorsTable.Append("<td>" + constructorsCollection.Constructors[i].ConstructorID + "</td>");
                    constructorsTable.Append("<td>" + constructorsCollection.Constructors[i].Url + "</td>");
                    constructorsTable.Append("<td>" + constructorsCollection.Constructors[i].Name + "</td>");
                    constructorsTable.Append("<td>" + constructorsCollection.Constructors[i].Nationality + "</td>");
                    constructorsTable.Append("</tr>");
                }
                constructorsTable.Append("</table>");
                PlaceHolder2.Controls.Add(new Literal { Text = constructorsTable.ToString() });

                //RACES 
                racesTable.Append("<h1>RACES</h1>");
                racesTable.Append("<table border ='1'>");
                racesTable.Append("<tr><th>Race name</th><th>Circuit name</th><th>url</th><th>Season</th><th>Round</th><th>Date</th><th>Time</th>");
                racesTable.Append("</tr>");

                for (var i = 0; i < racesCollection.Races.Count; i++)
                {
                    racesTable.Append("<tr>");
                    racesTable.Append("<td>" + racesCollection.Races[i].RaceName + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Circuit.CircuitName + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Url + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Season + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Round + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Date + "</td>");
                    racesTable.Append("<td>" + racesCollection.Races[i].Time + "</td>");
                    racesTable.Append("</tr>");
                }
                racesTable.Append("</table>");
                PlaceHolder3.Controls.Add(new Literal { Text = racesTable.ToString() });

                //CIRCUITS
                circuitsTable.Append("<h1>CIRCUITS</h1>");
                circuitsTable.Append("<table border ='1'>");
                circuitsTable.Append("<tr><th>Circuit ID</th><th>url</th><th>Circuit Name</th><th>Location</th><th>Country</th>");
                circuitsTable.Append("</tr>");

                for (var i = 0; i < circuitsCollection.Circuits.Count; i++)
                {
                    circuitsTable.Append("<tr>");
                    circuitsTable.Append("<td>" + circuitsCollection.Circuits[i].CircuitId + "</td>");
                    circuitsTable.Append("<td>" + circuitsCollection.Circuits[i].Url + "</td>");
                    circuitsTable.Append("<td>" + circuitsCollection.Circuits[i].CircuitName + "</td>");
                    circuitsTable.Append("<td>" + circuitsCollection.Circuits[i].Location.Locality + "</td>");
                    circuitsTable.Append("<td>" + circuitsCollection.Circuits[i].Location.Country + "</td>");
                    circuitsTable.Append("</tr>");
                }
                circuitsTable.Append("</table>");
                PlaceHolder4.Controls.Add(new Literal { Text = circuitsTable.ToString() });

                //RESULTS
                int y = racesCollection.Races.Count;
                int n = driversCollection.Drivers.Count;
                resultsTable.Append("<h1>RESULTS</h1>");
                resultsTable.Append("<table border ='1'>");
                resultsTable.Append("<tr><th>Constructor ID</th><th>Race name</th><th>Circuit ID</th><th>Driver ID</th><th>Number</th><th>Position</th><th>Points</th><th>Grid</th><th>Laps</th><th>Status</th>");
                resultsTable.Append("</tr>");

                for (int m = 0; m < y; m++) //teller antall sets av resultater
                {
                    for (var i = 0; i < n; i++)
                    {
                        resultsTable.Append("<tr>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Constructor.ConstructorId + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].RaceName + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Circuit.CircuitId + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Driver.DriverId + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Number + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Position + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Points + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Grid + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Laps + "</td>");
                        resultsTable.Append("<td>" + racesCollection.Races[m].Results[i].Status + "</td>");
                        resultsTable.Append("</tr>");
                    }
                }
                resultsTable.Append("</table>");
                PlaceHolder5.Controls.Add(new Literal { Text = resultsTable.ToString() });
            }

        }

        private static void UpdateDB(DriversCollection driversCollection, ConstructorsCollection constructorsCollection, RacesCollection racesCollection, CircuitsCollection circuitsCollection, MySqlCommand cmd)
        {

            //tømmer tabellene
            cmd.CommandText = "DELETE FROM ResultsTest";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM RacesTest";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM DriversTest";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM ConstructorsTest";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM CircuitsTest";
            cmd.ExecuteNonQuery();

            //cmd.CommandText setter kommandolinje teksten til spørringer. Husk ingen semicolon, den gjør det automatisk
            cmd.CommandText = "INSERT INTO DriversTest(driverId, url, givenName, familyName, dateOfBirth, nationality) VALUES(@driverId, @url, @givenName, @familyName, @dateOfBirth, @nationality)";

            int t = driversCollection.Drivers.Count;
            Debug.WriteLine("DriversCount: " + t);

            for (int i = 0; i < t; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@driverId", @driversCollection.Drivers[i].DriverID);
                cmd.Parameters.AddWithValue("@url", @driversCollection.Drivers[i].Url);
                cmd.Parameters.AddWithValue("@givenName", @driversCollection.Drivers[i].GivenName);
                cmd.Parameters.AddWithValue("@familyName", @driversCollection.Drivers[i].FamilyName);
                cmd.Parameters.AddWithValue("@dateOfBirth", @driversCollection.Drivers[i].DateOfBirth);
                cmd.Parameters.AddWithValue("@nationality", @driversCollection.Drivers[i].Nationality);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Row added to Drivers");
            }

            cmd.CommandText = "INSERT INTO ConstructorsTest(constructorId, url, name, nationality) VALUES(@constructorId, @url, @name, @nationality)";

            int c = constructorsCollection.Constructors.Count;
            Debug.WriteLine("ConstructorsCount: " + c);

            for (int i = 0; i < c; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@constructorId", @constructorsCollection.Constructors[i].ConstructorID);
                cmd.Parameters.AddWithValue("@url", @constructorsCollection.Constructors[i].Url);
                cmd.Parameters.AddWithValue("@name", @constructorsCollection.Constructors[i].Name);
                cmd.Parameters.AddWithValue("@nationality", @constructorsCollection.Constructors[i].Nationality);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Row added to Constructors");
            }

            cmd.CommandText = "INSERT INTO CircuitsTest(circuitId, url, circuitName, location, country) VALUES(@circuitId, @url, @circuitName, @location, @country)";

            int x = circuitsCollection.Circuits.Count;
            Debug.WriteLine("CircuitsCount: " + x);

            for (int i = 0; i < x; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@circuitId", @circuitsCollection.Circuits[i].CircuitId);
                cmd.Parameters.AddWithValue("@url", @circuitsCollection.Circuits[i].Url);
                cmd.Parameters.AddWithValue("@circuitName", @circuitsCollection.Circuits[i].CircuitName);
                cmd.Parameters.AddWithValue("@location", @circuitsCollection.Circuits[i].Location.Locality);
                cmd.Parameters.AddWithValue("@country", @circuitsCollection.Circuits[i].Location.Country);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Row added to Circuits");
            }

            cmd.CommandText = "INSERT INTO RacesTest(raceName, circuitId, url, season, round, date, time) VALUES(@raceName, @circuitId, @url, @season, @round, @date, @time)";

            int y = racesCollection.Races.Count;
            Debug.WriteLine("RacesCount: " + y);

            for (int i = 0; i < y; i++)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@raceName", @racesCollection.Races[i].RaceName);
                cmd.Parameters.AddWithValue("@circuitId", @racesCollection.Races[i].Circuit.CircuitId);
                cmd.Parameters.AddWithValue("@url", @racesCollection.Races[i].Url);
                cmd.Parameters.AddWithValue("@season", @racesCollection.Races[i].Season);
                cmd.Parameters.AddWithValue("@round", @racesCollection.Races[i].Round);
                cmd.Parameters.AddWithValue("@date", @racesCollection.Races[i].Date);
                cmd.Parameters.AddWithValue("@time", @racesCollection.Races[i].Time);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Debug.WriteLine("Row added to Races");
            }

            cmd.CommandText = "INSERT INTO ResultsTest(resultId, constructorId, raceName, circuitId, driverId, number, position, points, grid, laps, status) VALUES(DEFAULT, @constructorId, @raceName, @circuitId, @driverId, @number, @position, @points, @grid, @laps, @status)";

            for (int m = 0; m < y; m++)
            { //teller antall sets av resultater

                Debug.WriteLine("m is now " + m);
                //int n = racesCollection.Races[m].Results.Count; kan telle antall resultater for hvert løp, men ikke nødvendig siden antall drivers er konstant

                for (int i = 0; i < t; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@constructorId", @racesCollection.Races[m].Results[i].Constructor.ConstructorId);
                    cmd.Parameters.AddWithValue("@raceName", @racesCollection.Races[m].RaceName);
                    cmd.Parameters.AddWithValue("@circuitId", @racesCollection.Races[m].Circuit.CircuitId);
                    cmd.Parameters.AddWithValue("@driverId", @racesCollection.Races[m].Results[i].Driver.DriverId);
                    cmd.Parameters.AddWithValue("@number", @racesCollection.Races[m].Results[i].Number);
                    cmd.Parameters.AddWithValue("@position", @racesCollection.Races[m].Results[i].Position);
                    cmd.Parameters.AddWithValue("@points", @racesCollection.Races[m].Results[i].Points);
                    cmd.Parameters.AddWithValue("@grid", @racesCollection.Races[m].Results[i].Grid);
                    cmd.Parameters.AddWithValue("@laps", @racesCollection.Races[m].Results[i].Laps);
                    cmd.Parameters.AddWithValue("@status", @racesCollection.Races[m].Results[i].Status);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    Debug.WriteLine("Result for driver " + i);
                    Debug.WriteLine("Row added for Race " + m);
                }

            }

        }

        private static MySqlCommand EstablishConnectionToDB()
        {
            //Etablerer tilkobling
            string cs = @"server=256328.db.tornado-node.net;userid=mysql256328;password=txLmaLCLr38q8q8M!vbfV;database=mysql256328";
            var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            return cmd;
        }

        private static JObject FormatJSON(ref string JSON)
        {
            //Denne formaterer bare innkommende .json filer for å fjerne unødvendige deler og gjør den leselig for Newtonsoft.json
            JSON = JSON.Remove(JSON.Length - 1, 1);
            JSON = JSON.Remove(0, 9);
            JSON = JSON.Replace(@"\/", "/");
            JSON = JSON.Replace(@"\", string.Empty);
            JObject data = JObject.Parse(JSON);
            return data;
        }
        private static DriversCollection GetDriversFromAPI(WebClient webClient)
        {
            //Henter API, formaterer
            String JSON = webClient.DownloadString("http://ergast.com/api/f1/current/drivers.json?callback=myParser&limit=10000");
            JObject data = FormatJSON(ref JSON);
            //deserialize informasjonen inn i driversCollection og hopper over MRData og DriverTable i .json filen, returnerer deretter samlingen
            DriversCollection driversCollection = JsonConvert.DeserializeObject<DriversCollection>(data["MRData"]["DriverTable"].ToString());
            return driversCollection;
        }

        private static ConstructorsCollection GetConstructorsFromAPI(WebClient webClient)
        {
            String JSON = webClient.DownloadString("http://ergast.com/api/f1/current/constructors.json?callback=myParser&limit=10000");
            JObject data = FormatJSON(ref JSON);
            ConstructorsCollection constructorsCollection = JsonConvert.DeserializeObject<ConstructorsCollection>(data["MRData"]["ConstructorTable"].ToString());
            return constructorsCollection;
        }
        private static RacesCollection GetRacesFromAPI(WebClient webClient)
        {
            String JSON = webClient.DownloadString("https://ergast.com/api/f1/current/results.json?callback=myParser&limit=10000");
            JObject data = FormatJSON(ref JSON);
            RacesCollection racesCollection = JsonConvert.DeserializeObject<RacesCollection>(data["MRData"]["RaceTable"].ToString());
            Console.WriteLine("Complete");
            return racesCollection;
        }
        private static CircuitsCollection GetCircuitsFromAPI(WebClient webClient)
        {
            String JSON = webClient.DownloadString("http://ergast.com/api/f1/current/circuits.json?callback=myParser&limit=10000");
            JObject data = FormatJSON(ref JSON);
            CircuitsCollection circuitsCollection = JsonConvert.DeserializeObject<CircuitsCollection>(data["MRData"]["CircuitTable"].ToString());
            return circuitsCollection;
        }
    }
}