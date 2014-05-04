using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class NewsWrapperModel
    {
        public IList<News> news { get; set; }
        public IList<Photo> photos { get; set; }

        public NewsWrapperModel(IList<News> news, IList<Photo> photos)
        {
            this.photos = photos;
            this.news = news;
        }
    }
}