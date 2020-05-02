using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JSONAPI
{
    /* Mellomledd mellom ReadJSON.aspx.cs og Races.cs. 
       Her finner man samlingen av klassene i Races.cs
       som brukes som instans i ReadJSON.aspx.cs 
       for å laste opp til database og aksessere 
       informasjonen lagret fra .json APIen */
    public class RacesCollection
    {
        private List<Races> races;
        public List<Races> Races { get => races; set => races = value; }
    }
}
