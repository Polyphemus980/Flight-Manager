using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Crew : AbstractProduct
    {
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }

        public Crew(UInt64 ID_, string Name_, UInt64 Age_, string Phone_, string Email_, UInt16 Practice_, string Role_)
        {
            ID = ID_; Name = Name_; Age = Age_; Phone = Phone_; Email = Email_; Practice = Practice_; Role = Role_;
        }

    }
    public class CrewFactory : Factory
    {
        public AbstractProduct createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt64 Age = UInt64.Parse(list[2]);
            UInt16 Practice = UInt16.Parse(list[5]);
            return new Crew(ID, list[1], Age, list[3], list[4], Practice, list[6]);
        }
    }
}
