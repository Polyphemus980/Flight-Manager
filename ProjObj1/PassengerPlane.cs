using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class PassengerPlane : Airplane,IEntity
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 EconomicClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }

        public PassengerPlane(UInt64 ID, string Serial, string Country, string Model, UInt16 FirstClassSize, UInt16 EconomicClassSize, UInt16 BusinessClassSize): base(ID, Serial, Country, Model)
        {
            this.FirstClassSize = FirstClassSize; 
            this.EconomicClassSize = EconomicClassSize; 
            this.BusinessClassSize = BusinessClassSize;
        }
    }
    public class PassengerPlaneFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            (UInt64 ID, string Serial, string Country, string Model, UInt16 FirstClassSize, UInt16 EconomicClassSize, UInt16 BusinessClassSize) = PassengerPlaneParser.StringParser(list);
            return new PassengerPlane(ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }

        public IEntity CreateInstance(byte[] bytes)
        {

            (UInt64 ID, string Serial, string Country, string Model, UInt16 FirstClassSize, UInt16 EconomicClassSize, UInt16 BusinessClassSize) = PassengerPlaneParser.CargoPlaneParserBytes(bytes);
            return new PassengerPlane(ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
    }
    public static class PassengerPlaneParser
    {
        public static (UInt64,string,string,string,UInt16,UInt16,UInt16) StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            UInt16 FirstClassSize = UInt16.Parse(list[4]);
            UInt16 EconomicClassSize = UInt16.Parse(list[5]);
            UInt16 BusinessClassSize = UInt16.Parse(list[6]);
            return (ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
        public static (UInt64, string, string, string, UInt16,UInt16,UInt16) CargoPlaneParserBytes(Byte[] bytes)
        {
            UInt64 ID;
            UInt16 FirstClassSize,EconomicClassSize,BusinessClassSize;
            string Serial, Country, Model;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    ID = reader.ReadUInt64();

                    int SerialLength = 10;
                    byte[] SerialBytes = reader.ReadBytes(SerialLength);
                    Serial = Encoding.ASCII.GetString(SerialBytes).Trim('\0');

                    int CountryLength = 3;
                    byte[] CountryBytes = reader.ReadBytes(CountryLength);
                    Country = Encoding.ASCII.GetString(CountryBytes);

                    UInt16 ModelLength = reader.ReadUInt16();
                    byte[] ModelBytes = reader.ReadBytes(ModelLength);
                    Model = Encoding.ASCII.GetString(ModelBytes);

                    FirstClassSize = reader.ReadUInt16();
                    BusinessClassSize = reader.ReadUInt16();
                    EconomicClassSize = reader.ReadUInt16();

                }
            }
            return (ID, Serial, Country, Model, FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
    }
}
