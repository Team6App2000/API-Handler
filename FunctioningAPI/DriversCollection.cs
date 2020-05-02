using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JSONAPI
{    
    /* Mellomledd mellom ReadJSON.aspx.cs og Drivers.cs. 
       Her finner man samlingen av klassene i Drivers.cs
       som brukes som instans i ReadJSON.aspx.cs 
       for å laste opp til database og aksessere 
       informasjonen lagret fra .json APIen */
    public class DriversCollection
    {
        private List<Drivers> drivers;

        public List<Drivers> Drivers { get => drivers; set => drivers = value; }
    }
}
