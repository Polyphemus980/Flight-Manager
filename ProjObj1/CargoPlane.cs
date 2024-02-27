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

        public CargoPlane(UInt64 ID_, string Serial_, string Country_, string Model_, Single MaxLoad_):base(ID_,Serial_,Country_,Model_)
        {
            MaxLoad = MaxLoad_;
        }
    }
    public class CargoPlaneFactory : IFactory
    {
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            Single MaxLoad = Single.Parse(list[list.Length - 1], CultureInfo.InvariantCulture);
            return new CargoPlane(ID, list[1], list[2], list[3], MaxLoad);
        }
    }
}
