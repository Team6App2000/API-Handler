using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONAPI
{
    /*
    Filen inneholder bare klasser for informasjon som blir hentet
    fra API og mapper ut strukturen til selve .json filen fra API. 
    Newtonsoft.json håndterer getting og setting av
    disse automatisk når man deserializer.
    */
    public class Races
    {
        int season;
        string round;
        string url;
        string raceName;
        string date;
        string time;

        public int Season { get => season; set => season = value; }
        public string Round { get => round; set => round = value; }
        public string Url { get => url; set => url = value; }
        public string RaceName { get => raceName; set => raceName = value; }
        public string Date { get => date; set => date = value; }
        public string Time { get => time; set => time = value; }
        public Circuit Circuit { get; set; }
        public IList<Results> Results { get; set; }

    }
    public class Circuit
    {
        string circuitId;
        string url;
        string circuitName;

        public string CircuitId { get => circuitId; set => circuitId = value; }
        public string Url { get => url; set => url = value; }
        public string CircuitName { get => circuitName; set => circuitName = value; }
        public RaceLocation Location { get; set; }
    }
    public class Results
    {
        int number;
        int position;
        int points;
        int grid;
        int laps;
        string positionText;
        string status;
        public int Number { get => number; set => number = value; }
        public int Position { get => position; set => position = value; }
        public string PositionText { get => positionText; set => positionText = value; }
        public int Points { get => points; set => points = value; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public int Grid { get => grid; set => grid = value; }
        public int Laps { get => laps; set => laps = value; }
        public string Status { get => status; set => status = value; }
        public ResultsTime Time { get; set; }
        public FastestLap FastestLap { get; set; }

    }
    public class Driver
    {
        string driverId;
        public string DriverId { get => driverId; set => driverId = value; }
    }
    public class Constructor
    {
        string constructorId;
        public string ConstructorId { get => constructorId; set => constructorId = value; }
    }
    public class ResultsTime
    {
        int millis;
        string time;
        public int Millis { get => millis; set => millis = value; }
        public string Time { get => time; set => time = value; }
    }
    public class FastestLap
    {
        int rank;
        int lap;
        public int Rank { get => rank; set => rank = value; }
        public int Lap { get => lap; set => lap = value; }

        public LapTime Time { get; set; }

        public AverageSpeed AverageSpeed { get; set; }

    }
    public class LapTime
    {
        string time;
        public string Time { get => time; set => time = value; }

    }
    public class AverageSpeed
    {
        string units;
        float speed;
        public string Units { get => units; set => units = value; }
        public float Speed { get => speed; set => speed = value; }

    }
    public class RaceLocation
    {
        string lat;
        string @long;
        string locality;
        string country;

        public string Lat { get => lat; set => lat = value; }
        public string Long { get => @long; set => @long = value; }
        public string Locality { get => locality; set => locality = value; }
        public string Country { get => country; set => country = value; }
    }
}