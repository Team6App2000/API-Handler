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
    public class Constructors
    {
        string constructorID;
        string url;
        string name;
        string nationality;

        public string ConstructorID { get => constructorID; set => constructorID = value; }
        public string Url { get => url; set => url = value; }
        public string Name { get => name; set => name = value; }
        public string Nationality { get => nationality; set => nationality = value; }
    }
}