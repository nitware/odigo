﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface ISubscriptionFactory
    {
        ISubscription Create(Subscription subscription);
    }
}
