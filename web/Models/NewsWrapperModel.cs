using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class NewsWrapperModel
    {
        public News news { get; set; }
        public IEnumerable<News> allnews { get; set; }

        public NewsWrapperModel(IEnumerable<News> allnews, News news)
        {
            this.news = news;
            this.allnews = allnews;
        }
    }
}