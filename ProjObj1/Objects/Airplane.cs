using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public abstract class Airplane
    {
        
        public UInt64 ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        
        public Airplane(UInt64 ID, string Serial,string Country,string Model)
        {
            this.ID = ID;
            this.Serial= Serial;
            this.Country = Country;
            this.Model = Model;
        }
    }
    
}
