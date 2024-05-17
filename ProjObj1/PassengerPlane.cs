using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class PassengerPlane : Airplane,IEntity,IReportable
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 EconomicClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }

        public PassengerPlane(UInt64 ID, string Serial, string Country, string Model, UInt16 FirstClassSize, UInt16 EconomicClassSize, UInt16 BusinessClassSize): base(ID, Serial, Country, Model)
        {
            this.FirstClassSize = FirstClassSize; 
            this.EconomicClassSize = EconomicClassSize; 
            this.BusinessClassSize = BusinessClassSize;
            values = new Dictionary<string, Func<IComparable>>()
            {
                { "ID", () => ID },
                { "Serial", () => Serial },
                { "Country", () => Country },
                { "Model", () => Model },
                { "FirstClassSize", () => FirstClassSize },
                { "EconomicClassSize", () => EconomicClassSize },
                { "BusinessClassSize", () => BusinessClassSize },
            };
        }

        public override string ToString()
        {
            return "PP";
        }

        public Dictionary<string, Func<IComparable>> values { get; set; }

        public void addToDatabase()
        {
            Database.AddPassengerPlane(this);
        }
        public void changeContactInfo(ulong ID, string emailAddress, string phoneNumber)
        {
            Database.NoContactInfo(ID);
        }
        public void changeID(ulong prevID, ulong newID)
        {
            Database.UpdatePassengerPlaneId(prevID,newID);
        }

        public string acceptReport(IReporter reporter)
        {
            return reporter.Report(this);
        }
    }
    public class PassengerPlaneFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            return PassengerPlaneParser.StringParser(list);
        }

        public IEntity CreateInstance(byte[] bytes)
        {

            return PassengerPlaneParser.ByteParser(bytes);
        }
    }
    public static class PassengerPlaneParser
    {
        public static PassengerPlane StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            UInt16 FirstClassSize = UInt16.Parse(list[4]);
            UInt16 EconomicClassSize = UInt16.Parse(list[5]);
            UInt16 BusinessClassSize = UInt16.Parse(list[6]);
            return new PassengerPlane(ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
        public static PassengerPlane ByteParser(Byte[] bytes)
        {
            UInt64 ID;
            UInt16 FirstClassSize,EconomicClassSize,BusinessClassSize;
            string Serial, Country, Model;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();

                    int SerialLength = 10;
                    Serial=new string(reader.ReadChars(SerialLength)).Trim('\0');
                    int CountryLength = 3;
                    Country=new string(reader.ReadChars(CountryLength));    
                    UInt16 ModelLength = reader.ReadUInt16();
                    Model = new string(reader.ReadChars(ModelLength));
                    FirstClassSize = reader.ReadUInt16();
                    BusinessClassSize = reader.ReadUInt16();
                    EconomicClassSize = reader.ReadUInt16();

                }
            }
            return new PassengerPlane(ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
    }
}
