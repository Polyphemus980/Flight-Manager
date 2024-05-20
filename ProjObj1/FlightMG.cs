using Mapsui.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class FlightMG
    {

        public FlightsGUIData flightData;
        

        public FlightMG()
        {
            flightData=new FlightsGUIData();
        }
        public void UpdateFlights()
        {
            List<FlightGUI> updatedList = new List<FlightGUI>();


            foreach (Flight flight in Database.GetAllFlights()) 
            { 
                FlightGUI data = new FlightGUIAdapter(flight, Database.Airports[flight.Origin], Database.Airports[flight.Target]); 
                updatedList.Add(data);
            }


            flightData.UpdateFlights(updatedList);
        }
        
    }
}
