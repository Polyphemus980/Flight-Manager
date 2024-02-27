using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Airport : IEntity
    {
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public string Country { get; set; }

        public Airport(UInt64 ID_, string Name_, string Code_, Single Longitude_, Single Latitude_, Single AMSL_, string Country_)
        {
            ID = ID_; Name = Name_; Code = Code_; Longitude = Longitude_; Latitude = Latitude_; AMSL = AMSL_; Country = Country_;
        }
    }
    public class AirportFactory : IFactory
    {
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            string Code = list[2];
            Single Longitude = Single.Parse(list[3], CultureInfo.InvariantCulture);
            Single Latitude = Single.Parse(list[4], CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(list[5], CultureInfo.InvariantCulture);
            string Country = list[6];
            return new Airport(ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
    }
}
