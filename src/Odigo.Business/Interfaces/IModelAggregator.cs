using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Business.Interfaces
{
    public interface IModelAggregator<T>
    {
        string Aggregate(List<T> models);
    }

    //public interface IModelAggregator
    //{
    //    string Aggregate<T>(List<T> models);
    //}


}
