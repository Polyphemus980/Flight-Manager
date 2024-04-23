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

        public override string ToString()
        {
            return "CA";
        }

        public void addToDatabase()
        {
            Database.AddCargo(this);
        }

        public void changeID(ulong prevID, ulong newID)
        {
            Database.UpdateCargoId(prevID,newID);
        }
    }
    public class CargoFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            return CargoParser.StringParser(list);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
           return CargoParser.ByteParser(bytes);
        }
    }
    public static class CargoParser
    {
        public static Cargo StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            Single Weigth = Single.Parse(list[1], CultureInfo.InvariantCulture);
            string Code = list[2];
            string Description = list[3];
            return new Cargo(ID, Weigth, Code, Description);
        }
        public static Cargo ByteParser(Byte[] bytes)
        {
            UInt64 ID;
            Single Weigth;
            string Code,Description;
            using (MemoryStream stream= new MemoryStream(bytes))
            {
                using (BinaryReader reader=new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();
                    Weigth = reader.ReadSingle();
                    int CodeLength = 6;
                    Code = new string(reader.ReadChars(CodeLength));
                    UInt16 DescriptionLength = reader.ReadUInt16();
                    Description= new string(reader.ReadChars(DescriptionLength));
                }
            }
            return new Cargo(ID, Weigth, Code,Description);
        }
    }
}
