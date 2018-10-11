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
    public class NewsService : INews
    {
        private readonly IRepository _da;

        public NewsService(IRepository da)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }

            _da = da;
        }

        public News GetBy(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("News ID");
            }

            try
            {
                return _da.GetModelBy<News, NEWS>(n => n.News_Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<News> GetTop(int number)
        {
            try
            {
                List<News> news = null;
                if (number > 0)
                {
                    news = _da.GetAll<News, NEWS>();
                    if (news != null && news.Count > 0)
                    {
                        news = news.OrderByDescending(n => n.Id).ToList();
                        news = news.Take(number).ToList();
                    }
                }

                return news;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<News>> GetTopAsync(int number)
        {
            try
            {
                Task<List<News>> news = null;
                List<News> returnedNews = null;
                if (number > 0)
                {
                    news = _da.GetAllAsync<News, NEWS>();

                    returnedNews = await news;

                    if (returnedNews != null && returnedNews.Count > 0)
                    {
                        returnedNews = returnedNews.OrderByDescending(n => n.Id).ToList();
                        returnedNews = returnedNews.Take(number).ToList();
                    }
                }

                return returnedNews;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
