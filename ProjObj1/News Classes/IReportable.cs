﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public interface IReportable
    {
        public string acceptReport(IReporter reporter);
    }
}
