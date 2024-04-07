using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public interface IReporter
    {
        public string name { get; set; }
        string Report(Airport airport);
        string Report(CargoPlane cargoPlane);
        string Report(PassengerPlane passengerPlane);
    }
}
