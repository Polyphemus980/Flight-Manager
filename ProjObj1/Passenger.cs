using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Passenger :Person,IEntity
    {
        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger(UInt64 ID, string Name, UInt64 Age, string Phone, string Email, string Class, UInt64 Miles):base(ID,Name,Age,Phone,Email)
        {
            this.Class = Class;
            this.Miles = Miles ;
        }

        public void accept()
        {
            Database.AddPassenger(this);
        }
    }

    public class PassengerFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            return PassengerParser.StringParser(list);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            return PassengerParser.ByteParser(bytes);
        }
    }
    public static class PassengerParser
    {
        public static Passenger StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            string Class = list[5];
            UInt64 Miles = UInt64.Parse(list[6]);
            return new Passenger(ID, Name, Age, Phone, Email, Class, Miles);
        }
        public static Passenger ByteParser(Byte[] bytes)
        {
            UInt64 ID, Miles;
            UInt16 Age;
            string Name, Phone, Email, Class;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    Name = new string(reader.ReadChars(NameLength));
                    Age = reader.ReadUInt16();
                    int PhoneLength = 12;
                    Phone=new string(reader.ReadChars(PhoneLength));
                    UInt16 EmailLength = reader.ReadUInt16();
                    Email=new string(reader.ReadChars(EmailLength));
                    UInt16 ClassLength = 1;
                    byte[] ClassByte = reader.ReadBytes(ClassLength);
                    Class = Encoding.ASCII.GetString(ClassByte);
                    Miles= reader.ReadUInt64();
                }

            }
            return new Passenger(ID, Name, Age, Phone, Email, Class, Miles);
        }
    }
}
