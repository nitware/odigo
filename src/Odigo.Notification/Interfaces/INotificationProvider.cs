using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odigo.Notification.Interfaces
{
    public interface INotificationProvider<T, TResult>
    {
        TResult Send(T t);
    }




}
