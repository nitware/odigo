using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface IQuickLinker
    {
        List<QuickLink> GetTop(int number);

        Task<List<QuickLink>> GetTopAsync(int number);
    }
}
