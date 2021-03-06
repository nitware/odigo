﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;

namespace Odigo.Business.Interfaces
{
    public interface INews
    {
        News GetBy(long id);
        List<News> GetTop(int number);

        Task<List<News>> GetTopAsync(int number);
    }


}
