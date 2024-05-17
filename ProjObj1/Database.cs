
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
        public static IReadOnlyDictionary<ulong, IEntity> Objects => objects.AsReadOnly();
        public static void AddAirport(Airport airport)
        {
            airports.TryAdd(airport.ID, airport);
            objects.TryAdd(airport.ID,airport);
            lock (subjects) 
            {
                subjects.Add(airport);
            }
        }

        public static ConcurrentDictionary<ulong, Flight> GetAllFlights()
        {
            return flights;
        }

        public static ConcurrentDictionary<ulong, Airport> GetAllAirports()
        {
            return airports;
        }

        public static ConcurrentDictionary<ulong, Crew> GetAllCrews()
        {
            return crews;
        }

        public static ConcurrentDictionary<ulong, Cargo> GetAllCargos()
        {
            return cargos;
        }

        public static ConcurrentDictionary<ulong, CargoPlane> GetAllCargoPlanes()
        {
            return cargoPlanes;
        }

        public static ConcurrentDictionary<ulong, Passenger> GetAllPassengers()
        {
            return passengers;
        }

        public static ConcurrentDictionary<ulong, PassengerPlane> GetAllPassengerPlanes()
        {
            return passengerPlanes;
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

        public static void UpdateContactInfo(ulong Id, string emailAddress, string phoneNumber)
        {
            if (!CheckID(Id))
                return;
            objects[Id].changeContactInfo(Id,emailAddress,phoneNumber);
        }
        public static void NoContactInfo(ulong ID)
        {
            UpdateLogs("Cannot Update - Object without contact info");
        }

        public static void UpdatePassengerContactInfo(ulong Id, string emailAddress, string phoneNumber)
        {
            UpdateLogs($"Passenger Contact Info Update - ID : {Id} , Previous email : {passengers[Id].Email} , Previous " +
                       $"phone number : {passengers[Id].Phone} , New email : {emailAddress} , New phone number : {phoneNumber}");
            passengers[Id].Phone = phoneNumber;
            passengers[Id].Email = emailAddress;
        }
        public static void UpdateCrewContactInfo(ulong Id, string emailAddress, string phoneNumber)
        {
            UpdateLogs($"Crew Contact Info Update - ID : {Id} , Previous email : {crews[Id].Email} , Previous " +
                       $"phone number : {crews[Id].Phone} , New email : {emailAddress} , New phone number : {phoneNumber}");
            crews[Id].Phone = phoneNumber;
            crews[Id].Email = emailAddress;
        }
        public static void UpdateFlightPosition(ulong Id, float Latitude, float Longitude, float AMSL)
        {
            if (!CheckID(Id))
                return;
            if (!flights.ContainsKey(Id))
            {
                UpdateLogs($"Object with ID : {Id} is not a flight");
                return;
            }
            UpdateLogs($"Flight Position Update - ID : {Id} , Previous position : " +
                       $"({flights[Id].Latitude},{flights[Id].Longitude}) , " +
                       $"New position : ({Latitude},{Longitude})");

            flights[Id].Longitude = Longitude;
            flights[Id].Latitude = Latitude;
            flights[Id].AMSL = AMSL;
            
        }
        
        private static bool CheckID(ulong ID)
        {
            if (!objects.ContainsKey(ID))
            {
                UpdateLogs($"Cannot Update - No object with ID : {ID}");
                return false;
            }
            return true;
        }

        public static void UpdateID(ulong previousId, ulong newId)
        {
            if (!CheckID(previousId))
                return;
            objects[previousId].changeID(previousId,newId);
        }

        public static void UpdateAirportId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            airports[previousId].ID = newId;
            airports.ChangeKey(previousId,newId);
            foreach (Flight flight in flights.Values)
            {
                if (flight.Origin == previousId)
                    flight.Origin = newId;
                if (flight.Target == previousId)
                    flight.Target = newId;
            }
            UpdateLogs($"Airport ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdateCrewId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            crews[previousId].ID = newId;
            crews.ChangeKey(previousId, newId);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.CrewIDs.Length; j++)
                {
                    if (flight.CrewIDs[j] == previousId)
                        flight.CrewIDs[j] = newId;
                }
            }
            UpdateLogs($"Crew ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdatePassengerId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            passengers[previousId].ID = newId;
            passengers.ChangeKey(previousId,newId);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.LoadIDs.Length; j++)
                {
                    if (flight.LoadIDs[j] == previousId)
                        flight.LoadIDs[j] = newId;
                }
            }
            UpdateLogs($"Passenger ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdateCargoId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            cargos[previousId].ID = newId;
            cargos.ChangeKey(previousId,newId);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.LoadIDs.Length; j++)
                {
                    if (flight.LoadIDs[j] == previousId)
                        flight.LoadIDs[j] = newId;
                }
            }
            UpdateLogs($"Cargo ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdateCargoPlaneId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            cargoPlanes[previousId].ID = newId;
            cargoPlanes.ChangeKey(previousId,newId);
            foreach (Flight flight in flights.Values)
            {
                if (flight.PlaneID == previousId)
                    flight.PlaneID = newId;
            }
            UpdateLogs($"Cargo Plane ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdatePassengerPlaneId(ulong previousId, ulong newId)
        {
            
            objects.ChangeKey(previousId,newId);
            passengerPlanes[previousId].ID = newId;
            passengerPlanes.ChangeKey(previousId,newId);
            foreach (Flight flight in flights.Values)
            {
                if (flight.PlaneID == previousId)
                    flight.PlaneID = newId;
            }
            UpdateLogs($"Passenger Plane ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdateFlightId(ulong previousId, ulong newId)
        {
            objects.ChangeKey(previousId,newId);
            flights[previousId].ID = newId;
            flights.ChangeKey(previousId,newId);
            UpdateLogs($"Flight ID Update - Previous ID: {previousId}, New ID: {newId}");
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
