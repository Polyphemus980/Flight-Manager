using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Crew :Person, IEntity
    {

        public UInt16 Practice { get; set; }
        public string Role { get; set; }

        public Crew(UInt64 ID, string Name, UInt64 Age, string Phone, string Email, UInt16 Practice, string Role):base(ID,Name,Age,Phone,Email)
        {
            this.Practice = Practice;
            this.Role = Role;
        }

    }
    public class CrewFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            (UInt64 ID, string Name, UInt64 Age,string Phone,string Email, UInt16 Practice, string Role)=CrewParser.StringParser(list);
            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            (UInt64 ID, string Name, UInt64 Age, string Phone, string Email, UInt16 Practice, string Role) = CrewParser.ByteParser(bytes);
            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }
    }
    public static class CrewParser
    {
        public static (UInt64,string,UInt64,string,string,UInt16,string) StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            UInt16 Practice = UInt16.Parse(list[5]);
            string Role = list[6];
            return (ID, Name, Age, Phone, Email, Practice, Role);
        }
        public static (UInt64, string,UInt64, string, string, UInt16,string) ByteParser(Byte[] bytes)
        {
            UInt64 ID;
            UInt16 Age, Practice;
            string Name,Phone,Email, Role;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream,new System.Text.ASCIIEncoding()))
                {
                    ID = reader.ReadUInt64();
                    UInt16 NameLength=reader.ReadUInt16();
                    Name = new string(reader.ReadChars(NameLength)); 
                    Age = reader.ReadUInt16();
                    int PhoneLength = 12;
                    Phone=new string(reader.ReadChars(PhoneLength));
                    UInt16 EmailLength=reader.ReadUInt16();
                    Email = new string(reader.ReadChars(EmailLength));
                    Practice = reader.ReadUInt16();
                    UInt16 RoleLength = 1;
                    Role=new string(reader.ReadChars(RoleLength));
                }

            }
            return (ID, Name,Age,Phone,Email,Practice,Role);
        }
    }
}
