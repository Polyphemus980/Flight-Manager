
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
            { "Airports", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdateAirportId },
                    { "Country", UpdateAirportCountry },
                    { "Code", UpdateAirportCode },
                    { "AMSL", UpdateAirportAMSL },
                    { "WorldPosition.Lat", UpdateAirportWorldPositionLat },
                    { "WorldPosition.Long", UpdateAirportWorldPositionLong },
                    { "Name", UpdateAirportName }
                }
            },
            { "Crews", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdateCrewId },
                    { "Practice", UpdateCrewPractice },
                    { "Role", UpdateCrewRole },
                    { "Name", UpdateCrewName },
                    { "Age", UpdateCrewAge },
                    { "Phone", UpdateCrewPhone },
                    { "Email", UpdateCrewEmail }
                } 
            },
            { "Passengers", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdatePassengerId },
                    { "Class", UpdatePassengerClass },
                    { "Miles", UpdatePassengerMiles },
                    { "Name", UpdatePassengerName },
                    { "Age", UpdatePassengerAge },
                    { "Phone", UpdatePassengerPhone },
                    { "Email", UpdatePassengerEmail }
                } 
            },
            { "Cargos", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdateCargoId },
                    { "Weight", UpdateCargoWeight },
                    { "Code", UpdateCargoCode },
                    { "Description", UpdateCargoDescription }
                } 
            },
            { "CargoPlanes", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdateCargoPlaneId },
                    { "Serial", UpdateCargoPlaneSerial },
                    { "Country", UpdateCargoPlaneCountry },
                    { "Model", UpdateCargoPlaneModel },
                    { "MaxLoad", UpdateCargoPlaneMaxLoad }
                } 
            },
            { "PassengerPlanes", new Dictionary<string, Action<ulong, IComparable>> 
                {
                    { "ID", UpdatePassengerPlaneId },
                    { "Serial", UpdatePassengerPlaneSerial },
                    { "Country", UpdatePassengerPlaneCountry },
                    { "Model", UpdatePassengerPlaneModel },
                    { "FirstClassSize", UpdatePassengerPlaneFirstClassSize },
                    { "EconomicClassSize", UpdatePassengerPlaneEconomicClassSize },
                    { "BusinessClassSize", UpdatePassengerPlaneBusinessClassSize }
                } 
            },
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
            foreach (Flight flight in flights.Values)
            {
                if (flight.Target == ID || flight.Origin == ID)
                {
                    flights.TryRemove(flight.ID,out _);
                }
            }
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
        public static void UpdateAirportCountry(ulong airportId, IComparable newValue)
        {
            string newCountry = (string)newValue;
            airports[airportId].Country = newCountry;
            UpdateLogs($"Airport Country Update - Airport ID: {airportId}, New Country: {newCountry}");
        }

        public static void UpdateAirportCode(ulong airportId, IComparable newValue)
        {
            string newCode = (string)newValue;
            airports[airportId].Code = newCode;
            UpdateLogs($"Airport Code Update - Airport ID: {airportId}, New Code: {newCode}");
        }

        public static void UpdateAirportAMSL(ulong airportId, IComparable newValue)
        {
            float newAMSL = (float)newValue;
            airports[airportId].AMSL = newAMSL;
            UpdateLogs($"Airport AMSL Update - Airport ID: {airportId}, New AMSL: {newAMSL}");
        }

        public static void UpdateAirportWorldPositionLat(ulong airportId, IComparable newValue)
        {
            float newLat = (float)newValue;
            airports[airportId].Latitude = newLat;
            UpdateLogs($"Airport World Position Latitude Update - Airport ID: {airportId}, New Latitude: {newLat}");
        }

        public static void UpdateAirportWorldPositionLong(ulong airportId, IComparable newValue)
        {
            float newLong = (float)newValue;
            airports[airportId].Longitude = newLong;
            UpdateLogs($"Airport World Position Longitude Update - Airport ID: {airportId}, New Longitude: {newLong}");
        }

        public static void UpdateAirportName(ulong airportId, IComparable newValue)
        {
            string newName = (string)newValue;
            airports[airportId].Name = newName;
            UpdateLogs($"Airport Name Update - Airport ID: {airportId}, New Name: {newName}");
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
        public static void UpdateCrewPractice(ulong crewId, IComparable newValue)
        {
            ushort newPractice = (ushort)newValue;
            crews[crewId].Practice = newPractice;
            UpdateLogs($"Crew Practice Update - Crew ID: {crewId}, New Practice: {newPractice}");
        }

        public static void UpdateCrewRole(ulong crewId, IComparable newValue)
        {
            string newRole = (string)newValue;
            crews[crewId].Role = newRole;
            UpdateLogs($"Crew Role Update - Crew ID: {crewId}, New Role: {newRole}");
        }

        public static void UpdateCrewName(ulong crewId, IComparable newValue)
        {
            string newName = (string)newValue;
            crews[crewId].Name = newName;
            UpdateLogs($"Crew Name Update - Crew ID: {crewId}, New Name: {newName}");
        }

        public static void UpdateCrewAge(ulong crewId, IComparable newValue)
        {
            ulong newAge = (ulong)newValue;
            crews[crewId].Age = newAge;
            UpdateLogs($"Crew Age Update - Crew ID: {crewId}, New Age: {newAge}");
        }

        public static void UpdateCrewPhone(ulong crewId, IComparable newValue)
        {
            string newPhone = (string)newValue;
            crews[crewId].Phone = newPhone;
            UpdateLogs($"Crew Phone Update - Crew ID: {crewId}, New Phone: {newPhone}");
        }

        public static void UpdateCrewEmail(ulong crewId, IComparable newValue)
        {
            string newEmail = (string)newValue;
            crews[crewId].Email = newEmail;
            UpdateLogs($"Crew Email Update - Crew ID: {crewId}, New Email: {newEmail}");
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
        public static void UpdatePassengerClass(ulong passengerId, IComparable newValue)
        {
            string newClass = (string)newValue;
            passengers[passengerId].Class = newClass;
            UpdateLogs($"Passenger Class Update - Passenger ID: {passengerId}, New Class: {newClass}");
        }

        public static void UpdatePassengerMiles(ulong passengerId, IComparable newValue)
        {
            ulong newMiles = (ulong)newValue;
            passengers[passengerId].Miles = newMiles;
            UpdateLogs($"Passenger Miles Update - Passenger ID: {passengerId}, New Miles: {newMiles}");
        }

        public static void UpdatePassengerName(ulong passengerId, IComparable newValue)
        {
            string newName = (string)newValue;
            passengers[passengerId].Name = newName;
            UpdateLogs($"Passenger Name Update - Passenger ID: {passengerId}, New Name: {newName}");
        }

        public static void UpdatePassengerAge(ulong passengerId, IComparable newValue)
        {
            ulong newAge = (ulong)newValue;
            passengers[passengerId].Age = newAge;
            UpdateLogs($"Passenger Age Update - Passenger ID: {passengerId}, New Age: {newAge}");
        }

        public static void UpdatePassengerPhone(ulong passengerId, IComparable newValue)
        {
            string newPhone = (string)newValue;
            passengers[passengerId].Phone = newPhone;
            UpdateLogs($"Passenger Phone Update - Passenger ID: {passengerId}, New Phone: {newPhone}");
        }

        public static void UpdatePassengerEmail(ulong passengerId, IComparable newValue)
        {
            string newEmail = (string)newValue;
            passengers[passengerId].Email = newEmail;
            UpdateLogs($"Passenger Email Update - Passenger ID: {passengerId}, New Email: {newEmail}");
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
        public static void UpdateCargoWeight(ulong cargoId, IComparable newValue)
        {
            float newWeight = (float)newValue;
            cargos[cargoId].Weight = newWeight;
            UpdateLogs($"Cargo Weight Update - Cargo ID: {cargoId}, New Weight: {newWeight}");
        }

        public static void UpdateCargoCode(ulong cargoId, IComparable newValue)
        {
            string newCode = (string)newValue;
            cargos[cargoId].Code = newCode;
            UpdateLogs($"Cargo Code Update - Cargo ID: {cargoId}, New Code: {newCode}");
        }

        public static void UpdateCargoDescription(ulong cargoId, IComparable newValue)
        {
            string newDescription = (string)newValue;
            cargos[cargoId].Description = newDescription;
            UpdateLogs($"Cargo Description Update - Cargo ID: {cargoId}, New Description: {newDescription}");
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
        public static void UpdateCargoPlaneSerial(ulong planeId, IComparable newValue)
        {
            string newSerial = (string)newValue;
            cargoPlanes[planeId].Serial = newSerial;
            UpdateLogs($"Cargo Plane Serial Update - Plane ID: {planeId}, New Serial: {newSerial}");
        }

        public static void UpdateCargoPlaneCountry(ulong planeId, IComparable newValue)
        {
            string newCountry = (string)newValue;
            cargoPlanes[planeId].Country = newCountry;
            UpdateLogs($"Cargo Plane Country Update - Plane ID: {planeId}, New Country: {newCountry}");
        }

        public static void UpdateCargoPlaneModel(ulong planeId, IComparable newValue)
        {
            string newModel = (string)newValue;
            cargoPlanes[planeId].Model = newModel;
            UpdateLogs($"Cargo Plane Model Update - Plane ID: {planeId}, New Model: {newModel}");
        }

        public static void UpdateCargoPlaneMaxLoad(ulong planeId, IComparable newValue)
        {
            float newMaxLoad = (float)newValue;
            cargoPlanes[planeId].MaxLoad = newMaxLoad;
            UpdateLogs($"Cargo Plane Max Load Update - Plane ID: {planeId}, New Max Load: {newMaxLoad}");
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
        public static void UpdatePassengerPlaneSerial(ulong planeId, IComparable newValue)
        {
            string newSerial = (string)newValue;
            passengerPlanes[planeId].Serial = newSerial;
            UpdateLogs($"Passenger Plane Serial Update - Plane ID: {planeId}, New Serial: {newSerial}");
        }

        public static void UpdatePassengerPlaneCountry(ulong planeId, IComparable newValue)
        {
            string newCountry = (string)newValue;
            passengerPlanes[planeId].Country = newCountry;
            UpdateLogs($"Passenger Plane Country Update - Plane ID: {planeId}, New Country: {newCountry}");
        }

        public static void UpdatePassengerPlaneModel(ulong planeId, IComparable newValue)
        {
            string newModel = (string)newValue;
            passengerPlanes[planeId].Model = newModel;
            UpdateLogs($"Passenger Plane Model Update - Plane ID: {planeId}, New Model: {newModel}");
        }

        public static void UpdatePassengerPlaneFirstClassSize(ulong planeId, IComparable newValue)
        {
            ushort newFirstClassSize = (ushort)newValue;
            passengerPlanes[planeId].FirstClassSize = newFirstClassSize;
            UpdateLogs($"Passenger Plane First Class Size Update - Plane ID: {planeId}, New First Class Size: {newFirstClassSize}");
        }

        public static void UpdatePassengerPlaneEconomicClassSize(ulong planeId, IComparable newValue)
        {
            ushort newEconomicClassSize = (ushort)newValue;
            passengerPlanes[planeId].EconomicClassSize = newEconomicClassSize;
            UpdateLogs($"Passenger Plane Economic Class Size Update - Plane ID: {planeId}, New Economic Class Size: {newEconomicClassSize}");
        }

        public static void UpdatePassengerPlaneBusinessClassSize(ulong planeId, IComparable newValue)
        {
            ushort newBusinessClassSize = (ushort)newValue;
            passengerPlanes[planeId].BusinessClassSize = newBusinessClassSize;
            UpdateLogs($"Passenger Plane Business Class Size Update - Plane ID: {planeId}, New Business Class Size: {newBusinessClassSize}");
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
