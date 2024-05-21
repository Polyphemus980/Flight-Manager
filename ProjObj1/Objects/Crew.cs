using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

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
            values = new Dictionary<string, Func<IComparable>>
            {
                { "ID", () => this.ID },
                { "Name", () => this.Name },
                { "Age", () => this.Age },
                { "Phone", () => this.Phone },
                { "Email", () => this.Email },
                { "Practice", () => this.Practice },
                { "Role", () => this.Role }
            };
        }

        public Dictionary<string, Func<IComparable>> values { get; set; }

        public void addToDatabase()
        {
            Database.AddCrew(this);
        }

        public void changeID(ulong prevID, ulong newID)
        {
            Database.UpdateCrewId(prevID,newID);
        }

        public void changeContactInfo(ulong ID, string emailAddress, string phoneNumber)
        {
            Database.UpdateCrewContactInfo(ID,emailAddress,phoneNumber);
        }

        public override string ToString()
        {
            return "C";
        }
    }
    public class CrewFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            return CrewParser.StringParser(list);
        }

        public IEntity CreateInstance(byte[] bytes)
        {
            return CrewParser.ByteParser(bytes);
        }
    }
    public static class CrewParser
    {
        public static Crew StringParser(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            UInt16 Practice = UInt16.Parse(list[5]);
            string Role = list[6];
            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }
        public static Crew ByteParser(Byte[] bytes)
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
            return new Crew(ID, Name,Age,Phone,Email,Practice,Role);
        }
    }
}
