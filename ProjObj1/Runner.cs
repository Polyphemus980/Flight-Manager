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
                flightManager.UpdateFlights();
                FlightTrackerGUI.Runner.UpdateGUI(flightManager.flightData);
                Thread.Sleep(1000);
            }
        }
        
    }
}