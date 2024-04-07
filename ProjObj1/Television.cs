using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Television : IReporter
    {
        public string name { get ; set; }

        public Television(string name) 
        {
            this.name = name;
        }
        public string Report(Airport airport)
        {
            return $"An image of {airport.Name} airport";
        }

        public string Report(CargoPlane cargoPlane)
        {
            return $"An image of {cargoPlane.Serial} cargo plane";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"An image of {passengerPlane.Serial} passenger plane";
        }
    }
}
