using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    internal class Runner
    {
        private IDataSource dataSource;
        private ServerHandler updateServer;
        private FlightMG flightManager;
        public Runner(IDataSource dataSource,string UpdateServerPath)
        {
            this.dataSource = dataSource;
            this.flightManager = new FlightMG();
            updateServer = new ServerHandler(UpdateServerPath, 100, 200);
        }

        public void Run()
        {
            dataSource.Start();
            Task.Run(() => ConsoleHandler.ConsoleReact(dataSource));
            updateServer.Start();
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