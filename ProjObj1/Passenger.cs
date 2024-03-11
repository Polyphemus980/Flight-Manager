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
    }

    public class PassengerFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            (UInt64 ID, string Name, UInt64 Age, string Phone, string Email, string Class, UInt64 Role) = PassengerParser.StringParse(list);
            return new Passenger(ID, Name, Age, Phone, Email, Class, Role);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, string Name, UInt64 Age, string Phone, string Email, string Class, UInt64 Role) = PassengerParser.CrewParserBytes(bytes);
            return new Passenger(ID, Name, Age, Phone, Email, Class, Role);
        }
    }
    public static class PassengerParser
    {
        public static (UInt64,string,UInt64,string,string,string,UInt64) StringParse(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            string Class = list[5];
            UInt64 Miles = UInt64.Parse(list[6]);
            return (ID, Name, Age, Phone, Email, Class, Miles);
        }
        public static (UInt64, string, UInt64, string, string,string, UInt64) CrewParserBytes(Byte[] bytes)
        {
            UInt64 ID, Miles;
            UInt16 Age;
            string Name, Phone, Email, Class;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    ID = reader.ReadUInt64();
                    UInt16 NameLength = reader.ReadUInt16();
                    byte[] NameBytes = reader.ReadBytes(NameLength);
                    Name = Encoding.ASCII.GetString(NameBytes);

                    Age = reader.ReadUInt16();
                    int PhoneLength = 12;
                    byte[] PhoneBytes = reader.ReadBytes(PhoneLength);
                    Phone = Encoding.ASCII.GetString(PhoneBytes);
                    UInt16 EmailLength = reader.ReadUInt16();
                    byte[] EmailBytes = reader.ReadBytes(EmailLength);
                    Email = Encoding.ASCII.GetString(EmailBytes);
                    UInt16 ClassLength = 1;
                    byte[] ClassByte = reader.ReadBytes(ClassLength);
                    Class = Encoding.ASCII.GetString(ClassByte);
                    Miles= reader.ReadUInt64();
                }

            }
            return (ID, Name, Age, Phone, Email, Class, Miles);
        }
    }
}
