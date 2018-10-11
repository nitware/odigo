using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Entities;
using Odigo.Model.Model;

namespace Odigo.Model.Translator
{
    public class NewsTranslator : BaseTranslator<News, NEWS>
    {
        //private StaffTranslator staffTranslator;

        //public NewsTranslator()
        //{
        //    staffTranslator = new StaffTranslator();
        //}

        public override News TranslateToModel(NEWS newsEntity)
        {
            try
            {
                News news = null;
                if (newsEntity != null)
                {
                    news = new News();
                    news.Id = newsEntity.News_Id;
                    news.Title = newsEntity.Title;
                    news.Description = newsEntity.Description;
                    news.Date = newsEntity.Date;
                    news.ImageFileUrl = newsEntity.Image_File_Url;
                    news.ImageTitle = newsEntity.Image_Title;
                    //news.Staff = staffTranslator.TranslateToModel(newsEntity.STAFF);
                    news.Venue = newsEntity.Venue;
                    news.Hour = newsEntity.Hour;
                    news.Minute = newsEntity.Minute;
                }

                return news;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override NEWS TranslateToEntity(News news)
        {
            try
            {
                NEWS newsEntity = null;
                if (news != null)
                {
                    newsEntity = new NEWS();
                    newsEntity.News_Id = news.Id;
                    newsEntity.Title = news.Title;
                    newsEntity.Description = news.Description;
                    newsEntity.Date = news.Date;
                    newsEntity.Image_File_Url = news.ImageFileUrl;
                    newsEntity.Image_Title = news.ImageTitle;
                    //newsEntity.Entered_By_Id = news.Staff.Id;
                    newsEntity.Venue = news.Venue;
                    newsEntity.Hour = news.Hour;
                    newsEntity.Minute = news.Minute;
                }

                return newsEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }
}
