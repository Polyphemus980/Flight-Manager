using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public abstract class Person
    {
        public UInt64 ID { get; set; }
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person(UInt64 ID, string Name, UInt64 Age, string Phone, string Email)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
            this.Phone = Phone;
            this.Email = Email;
        }
    }
}
