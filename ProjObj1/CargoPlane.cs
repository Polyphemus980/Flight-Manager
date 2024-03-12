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

            (UInt64 ID, string Serial, string Country, string Model, Single MaxLoad) = CargoPlaneParser.StringParser(list);         
            return new CargoPlane(ID, Serial, Country, Model, MaxLoad);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, string Serial, string Country, string Model, Single MaxLoad) = CargoPlaneParser.ByteParser(bytes);
            return new CargoPlane(ID, Serial, Country, Model, MaxLoad);
        }
    }
    public static class CargoPlaneParser
    {
        public static (UInt64,string,string,string,Single) StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            Single MaxLoad = Single.Parse(list[4], CultureInfo.InvariantCulture);
            return (ID,Serial, Country, Model, MaxLoad);
        }
        public static (UInt64, string, string, string,Single) ByteParser(Byte[] bytes)
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
            return (ID, Serial,Country,Model,MaxLoad);
        }
    }
}
