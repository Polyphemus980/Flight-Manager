using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class PassengerPlane : Airplane,IEntity
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 EconomicClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }

        public PassengerPlane(UInt64 ID_, string Serial_, string Country_, string Model_, UInt16 FirstClassSize_, UInt16 EconomicClassSize_, UInt16 BusinessClassSize_): base(ID_, Serial_, Country_, Model_)
        {
           FirstClassSize = FirstClassSize_; EconomicClassSize = EconomicClassSize_; BusinessClassSize = BusinessClassSize_;
        }
    }
    public class PassengerPlaneFactory : IFactory
    {
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            UInt16 FirstClassSize = UInt16.Parse(list[4]);
            UInt16 EconomicClassSize = UInt16.Parse(list[5]);
            UInt16 BusinessClassSize = UInt16.Parse(list[6]);
            return new PassengerPlane(ID, list[1], list[2], list[3], FirstClassSize, EconomicClassSize, BusinessClassSize);
        }
    }
}
