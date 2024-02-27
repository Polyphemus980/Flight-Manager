using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public interface IFactory
    {
        IEntity createClass(string[] list);
    }
}
