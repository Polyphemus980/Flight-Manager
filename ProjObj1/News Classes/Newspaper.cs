using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    internal class Newspaper : IReporter
    {
        
        public Newspaper(string name)
        {
            this.name = name;
        }

        public string name { get; set; }

        public string Report(Airport airport)
        {
            return $"{name} - A report from the {airport.Name} airport, {airport.Country}.";
        }

        public string Report(CargoPlane cargoPlane)
        {
            return $"{name} - An interview with the crew of {cargoPlane.Serial}.";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"{name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
        }
    }
}
