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
        public List<Flight> flights = new List<Flight>();
        public Dictionary<ulong, Airport> airports=new Dictionary<ulong, Airport>();
        public FlightsGUIData flightData = new FlightsGUIData();
        
        public FlightMG(List<Flight> flights,Dictionary<ulong,Airport> airports)
        {
            this.airports = airports;
            List<FlightGUI> flGUIs = new List<FlightGUI>();
            this.flights = flights;
        }
        public FlightMG()
        {
            flights = new List<Flight>();
        }
        public void UpdateFlights()
        {
            List<FlightGUI> updatedList = new List<FlightGUI>();
            lock (flights)
            {
                lock (airports)
                {
                    foreach (Flight flight in flights)
                    {
                        FlightGUI data = new FlightGUIAdapter(flight, airports[flight.Origin], airports[flight.Target]);
                        updatedList.Add(data);
                    }
                }
            }
            flightData.UpdateFlights(updatedList);
        }
        
    }
}
