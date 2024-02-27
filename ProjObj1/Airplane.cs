using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Airplane
    {
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        
        public Airplane(UInt64 ID_, string Serial_,string Country_,string Model_)
        {
            ID = ID_;Serial= Serial_;Country = Country_;Model = Model_;
        }
    }
}
