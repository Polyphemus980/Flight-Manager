using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class CargoPlane : Airplane,IEntity
    {
        public Single MaxLoad { get; set; }
        public CargoPlane(UInt64 ID, string Serial, string Country, string Model, Single MaxLoad):base(ID,Serial,Country,Model)
        {
            this.MaxLoad = MaxLoad;
        }
    }
    public class CargoPlaneFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {

            (UInt64 ID, string Serial, string Country, string Model, Single MaxLoad) = CargoPlaneParser.CargoPlaneParserString(list);         
            return new CargoPlane(ID, Serial, Country, Model, MaxLoad);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, string Serial, string Country, string Model, Single MaxLoad) = CargoPlaneParser.CargoPlaneParserBytes(bytes);
            return new CargoPlane(ID, Serial, Country, Model, MaxLoad);
        }
    }
    public static class CargoPlaneParser
    {
        public static (UInt64,string,string,string,Single) CargoPlaneParserString(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            Single MaxLoad = Single.Parse(list[4], CultureInfo.InvariantCulture);
            return (ID,Serial, Country, Model, MaxLoad);
        }
        public static (UInt64, string, string, string,Single) CargoPlaneParserBytes(Byte[] bytes)
        {
            UInt64 ID;
            Single MaxLoad;
            string Serial,Country,Model;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    ID = reader.ReadUInt64();

                    int SerialLength = 10;
                    byte[] SerialBytes=reader.ReadBytes(SerialLength);
                    Serial=Encoding.UTF8.GetString(SerialBytes);
                    int CountryLength = 3;
                    byte[] CountryBytes = reader.ReadBytes(CountryLength);
                    Country=Encoding.UTF8.GetString(CountryBytes);
                    UInt16 ModelLength=reader.ReadUInt16();
                    byte[] ModelBytes = reader.ReadBytes(ModelLength);
                    Model=Encoding.UTF8.GetString(ModelBytes);
                    MaxLoad= reader.ReadSingle();
                }
            }
            return (ID, Serial,Country,Model,MaxLoad);
        }
    }
}
