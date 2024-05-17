using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Airport : IEntity,IReportable
    {
        public UInt64 ID { get; set; }
        public Dictionary<string, Func<IComparable>> values { get; set; }
        public Dictionary<string, Func<string,IComparable>> parsers { get; set; }
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
            values = new Dictionary<string, Func<IComparable>>();
            values.Add("ID",GetID);
            values.Add("Country",GetCountry);
            values.Add("Code",GetCode);
            values.Add("AMSL",GetAMSL);
            values.Add("Latitude",GetLatitude);
            values.Add("Longitude",GetLongitude);
            values.Add("Name",GetName);
            parsers = new Dictionary<string, Func<string,IComparable>>();
            parsers .Add("ID",DataHandler.ParseUInt64);
            parsers .Add("Country",DataHandler.ParseString);
            parsers .Add("Code",DataHandler.ParseString);
            parsers .Add("AMSL",DataHandler.ParseFloat);
            parsers .Add("Latitude",DataHandler.ParseFloat);
            parsers .Add("Longitude",DataHandler.ParseFloat);
            parsers .Add("Name",DataHandler.ParseString);
        }
        public IComparable GetCountry()
        {
            return Country;
        }
        public  IComparable GetID()
        {
            return ID;
        }
        public  IComparable GetName()
        {
            return Name;
        }

        public IComparable GetCode()
        {
            return Code;
        }

        public IComparable GetLongitude()
        {
            return Longitude;
        }
        public IComparable GetLatitude()
        {
            return Latitude;
        }
        public IComparable GetAMSL()
        {
            return AMSL;
        }

        public override string ToString()
        {
            return "AI";
        }

        public void addToDatabase()
        {
            Database.AddAirport(this);
        }

        public void changeID(ulong prevID, ulong newID)
        {
            Database.UpdateAirportId(prevID,newID);
        }

        public void changeContactInfo(ulong ID, string emailAddress, string phoneNumber)
        {
            Database.NoContactInfo(ID);
        }

        public string acceptReport(IReporter reporter)
        {
            return reporter.Report(this);
        }
    }
    public class AirportFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            return AirportParser.StringParser(list);
        }
        public IEntity CreateInstance(byte[] bytes)
        {
            return AirportParser.ByteParser(bytes);

            
        }
    }
    
    public class AirportParser
    {
        public static Airport StringParser(string[] list)
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
        public static Airport ByteParser(byte[] bytes)
        {
            UInt64 ID;
            Single Longitude, Latitude, AMSL;
            string Name, Code, Country;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    Name = new string(reader.ReadChars(NameLength));
                    int CodeLength = 3;
                    Code = new string(reader.ReadChars(CodeLength));
                    Longitude = reader.ReadSingle();
                    Latitude = reader.ReadSingle();
                    AMSL = reader.ReadSingle();
                    int CountryLength = 3;
                    Country=new string(reader.ReadChars(CountryLength));
                }
            }
            return new Airport(ID, Name, Code, Longitude, Latitude, AMSL, Country);
        }
    }
}
