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
        private FlightMG flightManager;
        NewsGenerator NewsGenerator;
        public Runner(IDataSource dataSource)
        {
            this.dataSource = dataSource;
            this.flightManager = new FlightMG();
        }

        public void Run()
        {
            dataSource.Start();
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