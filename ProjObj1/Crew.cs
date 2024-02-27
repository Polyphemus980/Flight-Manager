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

        public Crew(UInt64 ID_, string Name_, UInt64 Age_, string Phone_, string Email_, UInt16 Practice_, string Role_):base(ID_,Name_,Age_,Phone_,Email_)
        {
            Practice = Practice_; Role = Role_;
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
