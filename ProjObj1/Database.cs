
using System.Collections.Concurrent;

namespace PROJOBJ1
{
    public static class Database
    {
        private static ConcurrentDictionary<ulong, Flight> flights = new ConcurrentDictionary<ulong, Flight>();
        private static  ConcurrentDictionary<ulong, Airport> airports = new ConcurrentDictionary<ulong, Airport>();
        private static  ConcurrentDictionary<ulong, Crew> crews = new ConcurrentDictionary<ulong, Crew>();
        private static  ConcurrentDictionary<ulong, Cargo> cargos = new ConcurrentDictionary<ulong, Cargo>();
        private static  ConcurrentDictionary<ulong, CargoPlane> cargoPlanes = new ConcurrentDictionary<ulong, CargoPlane>();
        private static  ConcurrentDictionary<ulong, Passenger> passengers = new ConcurrentDictionary<ulong, Passenger>();
        private static  ConcurrentDictionary<ulong, PassengerPlane> passengerPlanes = new ConcurrentDictionary<ulong, PassengerPlane>();
        public static List<IReportable> subjects=new List<IReportable>();
        private static ConcurrentDictionary<ulong, IEntity> objects = new ConcurrentDictionary<ulong, IEntity>();
        public static IReadOnlyDictionary<ulong, Flight> Flights => flights.AsReadOnly();
        public static IReadOnlyDictionary<ulong, Airport> Airports => airports.AsReadOnly();
        public static IReadOnlyDictionary<ulong, Crew> Crews => crews.AsReadOnly();
        public static IReadOnlyDictionary<ulong, Cargo> Cargos => cargos.AsReadOnly();
        public static IReadOnlyDictionary<ulong, CargoPlane> CargoPlanes => cargoPlanes.AsReadOnly();
        public static IReadOnlyDictionary<ulong, Passenger> Passengers => passengers.AsReadOnly();
        public static IReadOnlyDictionary<ulong, PassengerPlane> PassengerPlanes => passengerPlanes.AsReadOnly();
        public static void AddAirport(Airport airport)
        {
            airports.TryAdd(airport.ID, airport);
            objects.TryAdd(airport.ID,airport);
            lock (subjects) 
            {
                subjects.Add(airport);
            }
        }

        public static void AddFlight(Flight flight)
        {

            flights.TryAdd(flight.ID,flight);
            objects.TryAdd(flight.ID,flight);
        }

        public static void AddCrew(Crew crew)
        {
            crews.TryAdd(crew.ID,crew);
            objects.TryAdd(crew.ID,crew);
        }

        public static void AddCargo(Cargo cargo)
        {
                cargos.TryAdd(cargo.ID,cargo);
                objects.TryAdd(cargo.ID,cargo);
        }

        public static void AddCargoPlane(CargoPlane cargoPlane)
        {

                cargoPlanes.TryAdd(cargoPlane.ID,cargoPlane);
                lock (subjects)
                {
                    subjects.Add(cargoPlane);
                }
                objects.TryAdd(cargoPlane.ID,cargoPlane);
        }

        public static void AddPassenger(Passenger passenger)
        {
                passengers.TryAdd(passenger.ID,passenger);
                objects.TryAdd(passenger.ID,passenger);
        }

        public static void AddPassengerPlane(PassengerPlane passengerPlane)
        {
            passengerPlanes.TryAdd(passengerPlane.ID,passengerPlane);
            lock (subjects)
            {
                subjects.Add(passengerPlane);
            } 
            objects.TryAdd(passengerPlane.ID,passengerPlane);
        }

        public static int LogUsage = 0;

        public static void UpdateContactInfo(ulong Id, string emailAddres, string phoneNumber)
        {
            if (!objects.ContainsKey(Id))
            {
                UpdateLogs($"ContactInfo Update - No object with ID: {Id}");
                return;
            }
        }

        public static void UpdateFlightPosition(ulong Id, float Latitude, float Longitude, float AMSL)
        {
            if (!flights.ContainsKey(Id))
            {
                UpdateLogs($"FlightPosition Update - No object with ID: {Id}");
                return;
            }
            UpdateLogs($"FlightPosition Update - Previous position: ({flights[Id].Latitude},{flights[Id].Longitude} , " +
                       $"New position: ({Latitude},{Longitude})");

            flights[Id].Longitude = Longitude;
            flights[Id].Latitude = Latitude;
            flights[Id].AMSL = AMSL;
            
        }
        
        public static void UpdateId(ulong previousId,ulong newId)
        {
            if (!objects.ContainsKey(previousId))
            {
                UpdateLogs($"ID Update - No object with ID: {previousId}");
                return;
            }
            switch (objects[previousId].ToString())
            {
                case "AI":
                    airports.ChangeKey(previousId, newId); 
                    break;
                case "C":
                    crews.ChangeKey(previousId,newId);
                    break;
                case "P":
                    passengers.ChangeKey(previousId,newId);
                    break;
                case "CA":
                    cargos.ChangeKey(previousId,newId);
                    break;
                case "CP":
                    cargoPlanes.ChangeKey(previousId,newId);
                    break;
                case "PP":
                    passengerPlanes.ChangeKey(previousId,newId);
                    break;
                case "FL":
                    flights.ChangeKey(previousId,newId);
                    break;
            }
            UpdateLogs($"ID Update - Previous ID: {previousId}, New ID: {newId}");
            
        }
        private static void ChangeKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey previousKey, TKey newKey)
        {
            TValue value = dictionary[previousKey];
            dictionary.Remove(previousKey);    
            dictionary[newKey] = value;
        }

        private static void UpdateLogs(string newLog)
        {
            string path = DateTime.Now.ToString("yy-MM-dd")+".txt";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter writer = File.AppendText(path))
            {
                if (LogUsage == 0)
                {
                    writer.WriteLine($"New logs - Application start: {DateTime.Now.ToString("HH:mm")} ");
                    LogUsage++;
                }
                writer.WriteLine(newLog);
            }
        }
    }
}
