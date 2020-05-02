using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JSONAPI
{    
    /* Mellomledd mellom ReadJSON.aspx.cs og Constructors.cs. 
       Her finner man samlingen av klassene i Constructors.cs
       som brukes som instans i ReadJSON.aspx.cs 
       for å laste opp til database og aksessere 
       informasjonen lagret fra .json APIen */
    public class ConstructorsCollection
    {
        private List<Constructors> constructors;

        public List<Constructors> Constructors { get => constructors; set => constructors = value; }
    }
}
