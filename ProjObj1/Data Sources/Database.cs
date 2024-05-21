
using System.Collections.Concurrent;

namespace PROJOBJ1
{
    public static class Database
    {
        public static readonly Dictionary<string, Action<ulong>> deleteFunctions = new()
        {
            { "Flights", DeleteFlight },
            { "Airports", DeleteAirport },
            { "Crews", DeleteCrew },
            { "Cargos", DeleteCargo },
            { "CargoPlanes", DeleteCargoPlane },
            { "Passengers", DeletePassenger },
            { "PassengerPlanes", DeletePassengerPlane }
        };

        public static readonly Dictionary<string, Dictionary<string, Action<ulong,IComparable>>> updateFunctions = new()
        {
            { "Airports", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdateAirportId } } },
            { "Crews", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdateCrewId } } },
            { "Passengers", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdatePassengerId } } },
            { "Cargos", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdateCargoId } } },
            { "CargoPlanes", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdateCargoPlaneId } } },
            { "PassengerPlanes", new Dictionary<string, Action<ulong, IComparable>> { { "ID", UpdatePassengerPlaneId } } },
            { "Flights", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdateFlightId },
                    { "Origin", UpdateFlightOrigin },
                    { "Target", UpdateFlightTarget },
                    { "TakeoffTime", UpdateFlightTakeoffTime },
                    { "LandingTime", UpdateFlightLandingTime },
                    { "WorldPosition.Long", UpdateFlightLongitude },
                    { "WorldPosition.Lat", UpdateFlightLatitude },
                    { "AMSL", UpdateFlightAMSL },
                    { "PlaneID", UpdateFlightPlaneID }
                } 
            }
        };
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

        public static void UpdateAirportID(IComparable prevID,IComparable ID)
        {
            airports[(ulong)prevID].ID = (ulong)ID;
        }
        public static void DeleteFlight(ulong ID)
        {
            if (!flights.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove flight with ID = {ID} from flights dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove flight with ID = {ID} from objects dictionary");
            }
        }

        public static void DeleteAirport(ulong ID)
        {
            if (!airports.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove airport with ID = {ID} from airports dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove airport with ID = {ID} from objects dictionary");
            }
        }

        public static void DeleteCrew(ulong ID)
        {
            if (!crews.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove crew with ID = {ID} from crews dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove crew with ID = {ID} from objects dictionary");
            }
        }

        public static void DeleteCargo(ulong ID)
        {
            if (!cargos.TryRemove(ID, out _))
            {
                Console.WriteLine(cargos.Count);
                throw new Exception($"Cannot remove cargo with ID = {ID} from cargos dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove cargo with ID = {ID} from objects dictionary");
            }
        }

        public static void DeleteCargoPlane(ulong ID)
        {
            if (!cargoPlanes.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove cargo plane with ID = {ID} from cargo planes dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove cargo plane with ID = {ID} from objects dictionary");
            }
        }

        public static void DeletePassenger(ulong ID)
        {
            if (!passengers.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove passenger with ID = {ID} from passengers dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove passenger with ID = {ID} from objects dictionary");
            }
        }

        public static void DeletePassengerPlane(ulong ID)
        {
            if (!passengerPlanes.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove passenger plane with ID = {ID} from passenger planes dictionary");
            }

            if (!objects.TryRemove(ID, out _))
            {
                throw new Exception($"Cannot remove passenger plane with ID = {ID} from objects dictionary");
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
        public static void UpdateAirportId(ulong previousId, IComparable newId)
        {
        ulong newIdValue = (ulong)newId;
        objects.ChangeKey(previousId, newIdValue);
        airports[previousId].ID = newIdValue;
        airports.ChangeKey(previousId, newIdValue);
        foreach (Flight flight in flights.Values)
        {
            if (flight.Origin == previousId)
                flight.Origin = newIdValue;
            if (flight.Target == previousId)
                flight.Target = newIdValue;
        }
        UpdateLogs($"Airport ID Update - Previous ID: {previousId}, New ID: {newId}");
        }

        public static void UpdateCrewId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            crews[previousId].ID = newIdValue;
            crews.ChangeKey(previousId, newIdValue);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.CrewIDs.Length; j++)
                {
                    if (flight.CrewIDs[j] == previousId)
                        flight.CrewIDs[j] = newIdValue;
                }
            }
            UpdateLogs($"Crew ID Update - Previous ID: {previousId}, New ID: {newId}");
        }

        public static void UpdatePassengerId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            passengers[previousId].ID = newIdValue;
            passengers.ChangeKey(previousId, newIdValue);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.LoadIDs.Length; j++)
                {
                    if (flight.LoadIDs[j] == previousId)
                        flight.LoadIDs[j] = newIdValue;
                }
            }
            UpdateLogs($"Passenger ID Update - Previous ID: {previousId}, New ID: {newId}");
        }

        public static void UpdateCargoId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            cargos[previousId].ID = newIdValue;
            cargos.ChangeKey(previousId, newIdValue);
            foreach (Flight flight in flights.Values)
            {
                for (int j = 0; j < flight.LoadIDs.Length; j++)
                {
                    if (flight.LoadIDs[j] == previousId)
                        flight.LoadIDs[j] = newIdValue;
                }
            }
            UpdateLogs($"Cargo ID Update - Previous ID: {previousId}, New ID: {newId}");
        }

        public static void UpdateCargoPlaneId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            cargoPlanes[previousId].ID = newIdValue;
            cargoPlanes.ChangeKey(previousId, newIdValue);
            foreach (Flight flight in flights.Values)
            {
                if (flight.PlaneID == previousId)
                    flight.PlaneID = newIdValue;
            }
            UpdateLogs($"Cargo Plane ID Update - Previous ID: {previousId}, New ID: {newId}");
        }

        public static void UpdatePassengerPlaneId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            passengerPlanes[previousId].ID = newIdValue;
            passengerPlanes.ChangeKey(previousId, newIdValue);
            foreach (Flight flight in flights.Values)
            {
                if (flight.PlaneID == previousId)
                    flight.PlaneID = newIdValue;
            }
            UpdateLogs($"Passenger Plane ID Update - Previous ID: {previousId}, New ID: {newId}");
            }

        public static void UpdateFlightId(ulong previousId, IComparable newId)
        {
            ulong newIdValue = (ulong)newId;
            objects.ChangeKey(previousId, newIdValue);
            flights[previousId].ID = newIdValue;
            flights.ChangeKey(previousId, newIdValue);
            UpdateLogs($"Flight ID Update - Previous ID: {previousId}, New ID: {newId}");
        }
        public static void UpdateFlightOrigin(ulong flightId, IComparable newValue)
        {
            ulong newOrigin = (ulong)newValue;
            flights[flightId].Origin = newOrigin;
            UpdateLogs($"Flight Origin Update - Flight ID: {flightId}, New Origin: {newOrigin}");
        }

        public static void UpdateFlightTarget(ulong flightId, IComparable newValue)
        {
            ulong newTarget = (ulong)newValue;
            flights[flightId].Target = newTarget;
            UpdateLogs($"Flight Target Update - Flight ID: {flightId}, New Target: {newTarget}");
        }

        public static void UpdateFlightTakeoffTime(ulong flightId, IComparable newValue)
        {
            string newTakeoffTime = (string)newValue;
            flights[flightId].TakeoffTime = newTakeoffTime;
            UpdateLogs($"Flight Takeoff Time Update - Flight ID: {flightId}, New Takeoff Time: {newTakeoffTime}");
        }

        public static void UpdateFlightLandingTime(ulong flightId, IComparable newValue)
        {
            string newLandingTime = (string)newValue;
            flights[flightId].LandingTime = newLandingTime;
            UpdateLogs($"Flight Landing Time Update - Flight ID: {flightId}, New Landing Time: {newLandingTime}");
        }

        public static void UpdateFlightLongitude(ulong flightId, IComparable newValue)
        {
            float newLongitude = (float)newValue;
            flights[flightId].Longitude = newLongitude;
            UpdateLogs($"Flight Longitude Update - Flight ID: {flightId}, New Longitude: {newLongitude}");
        }

        public static void UpdateFlightLatitude(ulong flightId, IComparable newValue)
        {
            float newLatitude = (float)newValue;
            flights[flightId].Latitude = newLatitude;
            UpdateLogs($"Flight Latitude Update - Flight ID: {flightId}, New Latitude: {newLatitude}");
        }

        public static void UpdateFlightAMSL(ulong flightId, IComparable newValue)
        {
            float newAMSL = (float)newValue;
            flights[flightId].AMSL = newAMSL;
            UpdateLogs($"Flight AMSL Update - Flight ID: {flightId}, New AMSL: {newAMSL}");
        }

        public static void UpdateFlightPlaneID(ulong flightId, IComparable newValue)
        {
            ulong newPlaneID = (ulong)newValue;
            flights[flightId].PlaneID = newPlaneID;
            UpdateLogs($"Flight Plane ID Update - Flight ID: {flightId}, New Plane ID: {newPlaneID}");
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
