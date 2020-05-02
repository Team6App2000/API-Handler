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
    public class Circuits
    {
        string circuitId;
        string url;
        string circuitName;

        public string CircuitId { get => circuitId; set => circuitId = value; }
        public string Url { get => url; set => url = value; }
        public string CircuitName { get => circuitName; set => circuitName = value; }
        public Location Location { get; set; }
    }
    public class Location
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