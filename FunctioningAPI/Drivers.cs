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
    public class Drivers
    {
        string driverID;
        int permanentNumber;
        string code;
        string url;
        string givenName;
        string familyName;
        string dateOfBirth;
        string nationality;

        public string DriverID { get => driverID; set => driverID = value; }
        public int PermanentNumber { get => permanentNumber; set => permanentNumber = value; }
        public string Code { get => code; set => code = value; }
        public string Url { get => url; set => url = value; }
        public string GivenName { get => givenName; set => givenName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Nationality { get => nationality; set => nationality = value; }
    }
}