using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Person
    {
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person(UInt64 ID_,string Name_,UInt64 Age_,string Phone_,string Email_)
        {
            ID = ID_;Name= Name_;Age = Age_;Phone=Phone_;Email = Email_;
        }
    }
}
