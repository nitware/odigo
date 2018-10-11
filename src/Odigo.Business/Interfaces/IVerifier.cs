using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface IVerifier
    {
        bool Verify(Teacher teacher);
        bool UpdateStatus(Teacher teacher);
        TeacherVerificationStatus CheckStatus(Teacher teacher);
        
    }




}
