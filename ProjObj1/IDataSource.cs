using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public interface IDataSource
    {
        List<IEntity> objects {  get;set;}
        public void Start();
    }
}
