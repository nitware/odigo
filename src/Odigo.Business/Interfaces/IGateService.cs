using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface IGateService
    {
        LoginDetail Get(string userName, string password);
        bool ChangePassword(Person person, string password);
        byte[] CreatePasswordHash(string password);
    }


}
