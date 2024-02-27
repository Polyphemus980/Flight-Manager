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
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Age = UInt64.Parse(list[2]);
            UInt16 Practice = UInt16.Parse(list[5]);
            return new Crew(ID, list[1], Age, list[3], list[4], Practice, list[6]);
        }
    }
}
