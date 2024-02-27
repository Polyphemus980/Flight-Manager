using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Flight : IEntity
    {
        public UInt64 ID { get; set; }
        public UInt64 Origin { get; set; }
        public UInt64 Target { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewIDs { get; set; }
        public UInt64[] LoadIDs { get; set; }

        public Flight(UInt64 ID, UInt64 Origin, UInt64 Target, string TakeoffTime, string LandingTime, Single Longitude,
        Single Latitude, Single AMSL, UInt64 PlaneID, UInt64[] CrewIDs, UInt64[] LoadIDs)
        {
            this.ID = ID; 
            this.Origin = Origin; 
            this.Target = Target;
            this.TakeoffTime = TakeoffTime; 
            this.LandingTime = LandingTime; 
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.AMSL = AMSL;
            this.PlaneID = PlaneID; 
            this.CrewIDs = CrewIDs;
            this.LoadIDs = LoadIDs;
        }
    }
    public class FlightFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Origin = UInt64.Parse(list[1]);
            UInt64 Target = UInt64.Parse(list[2]);
            string TakeoffTime = list[3];
            string LandingTime = list[4];
            Single Longitude = Single.Parse(list[5], CultureInfo.InvariantCulture);
            Single Latitude = Single.Parse(list[6], CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(list[7], CultureInfo.InvariantCulture);
            UInt64 PlaneID = UInt64.Parse(list[8]);
            
            string[] CrewIDs = list[9].Trim('[', ']').Split(';');
            UInt64[] Crew = new UInt64[CrewIDs.Length];
            for (int i = 0; i < CrewIDs.Length; i++)
            {
                Crew[i] = UInt64.Parse(CrewIDs[i]);
            }

            string[] LoadIDs = list[10].Trim('[',']').Split(";");
            UInt64[] Load = new UInt64[LoadIDs.Length];
            for (int i = 0; i < LoadIDs.Length; i++)
            {
                Load[i] = UInt64.Parse(LoadIDs[i]);
            }

            return new Flight(ID, Origin, Target, TakeoffTime, LandingTime, Longitude, Latitude, AMSL, PlaneID, Crew, Load);
        }
    }
}
