using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROJOBJ1
{
    internal class Radio : IReporter
    {
        public Radio(string name)
        {
            this.name = name;
        }

        public string name { get; set; }

        public string Report(Airport airport)
        {
            return $"Reporting for {name},Ladies and gentelmen, we are at the {airport.Name} airport.";
        }

        public string Report(CargoPlane cargoPlane)
        {
            return $"Reporting for {name},Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"Reporting for {name},Ladies and gentelmen, we've just witnessed {passengerPlane.Serial} take off.";
        }
    }
}
