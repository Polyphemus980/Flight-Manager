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

        public Passenger(UInt64 ID_, string Name_, UInt64 Age_, string Phone_, string Email_, string Class_, UInt64 Role_):base(ID_,Name_,Age_,Phone_,Email_)
        {
         Class = Class_; Role = Role_;
        }
    }

    public class PassengerFactory : IFactory
    {
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Age = UInt64.Parse(list[2]);
            UInt64 Role = UInt64.Parse(list[list.Length - 1]);
            return new Passenger(ID, list[1], Age, list[3], list[4], list[5], Role);
        }
    }
}
