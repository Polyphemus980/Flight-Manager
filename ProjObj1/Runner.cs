using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    internal class Runner
    {
        private ServerHandler server;
        private FlightMG flightManager;
        List<IEntity> objects;
        public Runner(ServerHandler server)
        {
            this.server = server;
            flightManager = new FlightMG();
            objects = new List<IEntity>();
        }

        public void Run()
        {
            server.StartServer();
            Task.Run(() => FlightTrackerGUI.Runner.Run());
            while (true)
            {
                Refresh();
                flightManager.UpdateFlights();
                FlightTrackerGUI.Runner.UpdateGUI(flightManager.flightData);
                Thread.Sleep(1000);
            }
        }
        
        private void Refresh()
        {
            List<Flight> currentFlights;
            Dictionary<ulong, Airport> currentAirports;
            lock (server.visitor.flights)
            {
                currentFlights = new List<Flight>(server.visitor.flights);
            }
            lock (server.visitor.airports)
            {
               currentAirports = new Dictionary<ulong, Airport>(server.visitor.airports);
            }

            lock (flightManager.flights)
            {
                flightManager.flights = currentFlights;
            }
            lock (flightManager.airports)
            {
                flightManager.airports = currentAirports;
            }
        }
    }
}