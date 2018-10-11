using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Business.Interfaces
{
    public interface IUploader<T>
    {
        bool Upload(List<T> data);
        bool ModifyUpload(T data);
        bool DeleteUpload(T data);
    }


}
