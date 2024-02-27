using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class CargoPlane : Airplane,IEntity
    {
        public Single MaxLoad { get; set; }
        public CargoPlane(UInt64 ID, string Serial, string Country, string Model, Single MaxLoad):base(ID,Serial,Country,Model)
        {
            this.MaxLoad = MaxLoad;
        }
    }
    public class CargoPlaneFactory : IFactory
    {
        public IEntity CreateInstance(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            string Serial = list[1];
            string Country = list[2];
            string Model = list[3];
            Single MaxLoad = Single.Parse(list[4], CultureInfo.InvariantCulture);
            return new CargoPlane(ID, Serial, Country, Model, MaxLoad);
        }
    }
}
