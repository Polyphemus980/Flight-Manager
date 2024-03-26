using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Visitor
    {
        public List<Flight> flights=new List<Flight>();
        public Dictionary<ulong, Airport> airports=new Dictionary<ulong, Airport>();
        public void visitAirport(Airport airport)
        {
            lock (airports)
            {
                airports.Add(airport.ID, airport);
            }
        }
        public void visitFlight(Flight flight)
        {
            lock (flights)
            {
                flights.Add(flight);
            }
        }
        public void visitCrew(Crew crew)
        {
            return;
        }
        public void visitCargo(Cargo cargo)
        {
            return;
        }
        public void visitCargoPlane(CargoPlane cargoPlane)
        {
            return;
        }
        public void visitPassenger(Passenger passenger)
        {
            return;
        }
        public void visitPassengerPlane(PassengerPlane passengerPlane)
        {
            return;
        }
    }
}
