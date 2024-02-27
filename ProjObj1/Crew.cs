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
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            UInt16 Practice = UInt16.Parse(list[5]);
            string Role = list[6];
            return new Crew(ID, Name, Age, Phone, Email, Practice, Role);
        }
    }
}
