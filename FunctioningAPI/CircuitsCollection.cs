using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JSONAPI
{    
    /* Mellomledd mellom ReadJSON.aspx.cs og Circuits.cs. 
       Her finner man samlingen av klassene i Circuits.cs
       som brukes som instans i ReadJSON.aspx.cs 
       for å laste opp til database og aksessere 
       informasjonen lagret fra .json APIen */
    public class CircuitsCollection
    {
        private List<Circuits> circuits;
        public List<Circuits> Circuits { get => circuits; set => circuits = value; }
    }
}
