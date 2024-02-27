using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Passenger :Person,IEntity
    {
        public string Class { get; set; }
        public UInt64 Role { get; set; }

        public Passenger(UInt64 ID, string Name, UInt64 Age, string Phone, string Email, string Class, UInt64 Role):base(ID,Name,Age,Phone,Email)
        {
         this.Class = Class;
            this.Role = Role;
        }
    }

    public class PassengerFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Name = list[1];
            UInt64 Age = UInt64.Parse(list[2]);
            string Phone = list[3];
            string Email = list[4];
            string Class = list[5];
            UInt64 Role = UInt64.Parse(list[6]);
            return new Passenger(ID, Name, Age, Phone, Email, Class, Role);
        }
    }
}
