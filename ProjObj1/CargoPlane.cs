using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class CargoPlane : Airplane, IEntity,IReportable
    {
        public Single MaxLoad { get; set; }
        public CargoPlane(UInt64 ID, string Serial, string Country, string Model, Single MaxLoad):base(ID,Serial,Country,Model)
        {
            this.MaxLoad = MaxLoad;
            values = new Dictionary<string, Func<IComparable>>()
            {
                { "ID", () => ID },
                { "Serial", () => Serial },
                { "Country", () => Country },
                { "Model", () => Model },
                { "MaxLoad",()=>MaxLoad}
            };
        }

        public Dictionary<string, Func<IComparable>> values { get; set; }

        public void addToDatabase()
        {
            Database.AddCargoPlane(this);
        }
        public void changeContactInfo(ulong ID, string emailAddress, string phoneNumber)
        {
            Database.NoContactInfo(ID);
        }
        public void changeID(ulong prevID, ulong newID)
        {
            Database.UpdateCargoPlaneId(prevID,newID);
        }

        public override string ToString()
        {
            return "CP";
        }

        public string acceptReport(IReporter reporter)
        {
           return reporter.Report(this);
        }
    }
    public class CargoPlaneFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {

            return CargoPlaneParser.StringParser(list);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            return CargoPlaneParser.ByteParser(bytes);
        }
    }
    public static class CargoPlaneParser
    {
        public static CargoPlane StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            Single MaxLoad = Single.Parse(list[4], CultureInfo.InvariantCulture);
            return new CargoPlane(ID,Serial, Country, Model, MaxLoad);
        }
        public static CargoPlane ByteParser(Byte[] bytes)
        {
            UInt64 ID;
            Single MaxLoad;
            string Serial,Country,Model;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();

                    int SerialLength = 10;
                    Serial=new string(reader.ReadChars(SerialLength)).Trim('\0');
                    int CountryLength = 3;
                    Country=new string(reader.ReadChars(CountryLength)).Trim('\0');
                    UInt16 ModelLength=reader.ReadUInt16();
                    Model = new string(reader.ReadChars(ModelLength));
                    MaxLoad= reader.ReadSingle();
                }
            }
            return new CargoPlane(ID, Serial,Country,Model,MaxLoad);
        }
    }
}
