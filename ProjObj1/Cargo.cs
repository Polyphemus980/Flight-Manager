﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Cargo : IEntity
    {
        public UInt64 ID { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(ulong ID_, float Weight_, string Code_, string Description_)
        {
            ID = ID_;
            Weight = Weight_;
            Code = Code_;
            Description = Description_;
        }
    }
    public class CargoFactory : IFactory
    {
        public IEntity createClass(string[] list)
        {
            UInt64 ID = UInt64.Parse(list[0]);
            Single Weigth = Single.Parse(list[1], CultureInfo.InvariantCulture);
            string Code = list[2];
            string Description = list[3];
            return new Cargo(ID, Weigth, Code,Description);
        }
    }
}
