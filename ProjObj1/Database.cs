using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Database
    {
        public static List<Flight> flights = new List<Flight>();
        public static Dictionary<ulong, Airport> airports = new Dictionary<ulong, Airport>();
        public static List<Crew> crews = new List<Crew>();
        public static List<Cargo> cargos = new List<Cargo>();
        public static List<CargoPlane> cargoPlanes = new List<CargoPlane>();
        public static List<Passenger> passengers = new List<Passenger>();
        public static List<PassengerPlane> passengerPlanes = new List<PassengerPlane>();
        public static List<IReportable> subjects= new List<IReportable>();
        public static void AddAirport(Airport airport)
        {
            lock (airports)
            {
                airports.Add(airport.ID, airport);
            }
            lock (subjects) 
            {
                subjects.Add(airport);
            }
        }

        public static void AddFlight(Flight flight)
        {
            lock (flights)
            {
                flights.Add(flight);
            }
        }

        public static void AddCrew(Crew crew)
        {
            lock (crews)
            {
                crews.Add(crew);
            }
        }

        public static void AddCargo(Cargo cargo)
        {
            lock (cargos)
            {
                cargos.Add(cargo);
            }
        }

        public static void AddCargoPlane(CargoPlane cargoPlane)
        {
            lock (cargoPlanes)
            {
                cargoPlanes.Add(cargoPlane);
            }
            lock (subjects)
            {
                subjects.Add(cargoPlane);
            }
        }

        public static void AddPassenger(Passenger passenger)
        {
            lock (passengers)
            {
                passengers.Add(passenger);
            }
        }

        public static void AddPassengerPlane(PassengerPlane passengerPlane)
        {
            lock (passengerPlanes)
            {
                passengerPlanes.Add(passengerPlane);
            }
            lock (subjects)
            {
                subjects.Add(passengerPlane);
            }
        }
    }
}
