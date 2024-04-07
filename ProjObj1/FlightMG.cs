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

        public FlightsGUIData flightData = new FlightsGUIData();
        

        public FlightMG()
        {
            flightData=new FlightsGUIData();
        }
        public void UpdateFlights()
        {
            List<FlightGUI> updatedList = new List<FlightGUI>();
            lock (Database.flights)
            {
                lock (Database.airports)
                {
                    foreach (Flight flight in Database.flights)
                    {
                        FlightGUI data = new FlightGUIAdapter(flight, Database.airports[flight.Origin],Database.airports[flight.Target]);
                        updatedList.Add(data);
                    }
                }
            }
            flightData.UpdateFlights(updatedList);
        }
        
    }
}
