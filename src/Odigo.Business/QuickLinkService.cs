using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Entities;

namespace Odigo.Business
{
    public class QuickLinkService : IQuickLinker
    {
        private readonly IRepository _da;

        public QuickLinkService(IRepository da)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }

            _da = da;
        }

        public List<QuickLink> GetTop(int number)
        {
            try
            {
                List<QuickLink> quickLinks = _da.GetAll<QuickLink, QUICK_LINK>();
                if (quickLinks != null && quickLinks.Count > 0)
                {
                    quickLinks.Take(number);
                }

                return quickLinks;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<QuickLink>> GetTopAsync(int number)
        {
            try
            {
                Task<List<QuickLink>> quickLinks = null;
                List<QuickLink> returnedQuickLinks = null;
                if (number > 0)
                {
                    quickLinks = _da.GetAllAsync<QuickLink, QUICK_LINK>();

                    returnedQuickLinks = await quickLinks;

                    if (returnedQuickLinks != null && returnedQuickLinks.Count > 0)
                    {
                        returnedQuickLinks = returnedQuickLinks.OrderByDescending(n => n.Id).ToList();
                        returnedQuickLinks = returnedQuickLinks.Take(number).ToList();
                    }
                }

                return returnedQuickLinks;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
