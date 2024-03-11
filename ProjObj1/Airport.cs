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

        public Airport(UInt64 ID, string Name, string Code, Single Longitude, Single Latitude, Single AMSL, string Country)
        {
            this.ID = ID;
            this.Name = Name;
            this.Code = Code;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
            this.AMSL = AMSL;
            this.Country = Country;
        }
    }
    public class AirportFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            (UInt64 ID, string Name, string Code,Single Longitude,Single Latitude, Single AMSL,string Country)=AirportParser.AirportParserString(list);
            return new Airport(ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, string Name, string Code, Single Longitude, Single Latitude, Single AMSL, string Country) = AirportParser.AirportParserByte(bytes);
            return new Airport(ID, Name, Code, Longitude, Latitude, AMSL, Country);

            
        }
    }
    
    public static class AirportParser
    {
        public static (UInt64, string, string, Single, Single, Single, string) AirportParserString(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            string Code = list[2];
            Single Longitude = Single.Parse(list[3], CultureInfo.InvariantCulture);
            Single Latitude = Single.Parse(list[4], CultureInfo.InvariantCulture);
            Single AMSL = Single.Parse(list[5], CultureInfo.InvariantCulture);
            string Country = list[6];
            return (ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
        public static (UInt64, string, string, Single, Single, Single, string) AirportParserByte(byte[] bytes)
        {
            UInt64 ID;
            Single Longitude, Latitude, AMSL;
            string Name, Code, Country;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    ID = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    byte[] NameBytes = reader.ReadBytes(NameLength);
                    Name = Encoding.ASCII.GetString(NameBytes);
                    int CodeLength = 3;
                    byte[] CodeBytes = reader.ReadBytes(CodeLength);
                    Code = Encoding.ASCII.GetString(CodeBytes);
                    Longitude = reader.ReadSingle();
                    Latitude = reader.ReadSingle();
                    AMSL = reader.ReadSingle();
                    int CountryLength = 3;
                    byte[] CountryBytes = reader.ReadBytes(CountryLength);
                    Country = Encoding.ASCII.GetString(CountryBytes);
                }
            }
            return (ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
    }
}
