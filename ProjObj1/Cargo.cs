using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Cargo : IEntity
    {
        public UInt64 ID { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong ID, float Weight, string Code, string Description)
        {
            this.ID = ID;
            this.Weight = Weight;
            this.Code = Code;
            this.Description = Description;
        }
    }
    public class CargoFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            (UInt64 ID,Single Weigth,string Code,string Description)=CargoParser.CargoParserString(list);
            return new Cargo(ID, Weigth, Code, Description);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, Single Weigth, string Code, string Description) = CargoParser.CargoParserBytes(bytes);
            return new Cargo(ID , Weigth, Code, Description);
        }
    }
    public static class CargoParser
    {
        public static (UInt64, Single, string, string) CargoParserString(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            Single Weigth = Single.Parse(list[1], CultureInfo.InvariantCulture);
            string Code = list[2];
            string Description = list[3];
            return (ID, Weigth, Code, Description);
        }
        public static (UInt64, Single, string, string) CargoParserBytes(Byte[] bytes)
        {
            UInt64 ID;
            Single Weigth;
            string Code,Description;
            using (MemoryStream stream= new MemoryStream(bytes))
            {
                using (BinaryReader reader=new BinaryReader(stream))
                {
                    ID = reader.ReadUInt64();
                    Weigth = reader.ReadUInt64();
                    int CodeLength = 6;
                    byte[] CodeBytes = reader.ReadBytes(CodeLength);
                    Code = Encoding.UTF8.GetString(CodeBytes);
                    int DescriptionLength = 2;
                    byte[] DescriptionBytes=reader.ReadBytes(DescriptionLength);
                    Description= Encoding.UTF8.GetString(DescriptionBytes);
                }
            }
            return (ID, Weigth, Code,Description);
        }
    }
}
