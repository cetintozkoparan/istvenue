using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ProjectWrapperModel
    {
        public IEnumerable<Projects> ps { get; set; }
        public Projects p { get; set; }
        public List<Photo> photos { get; set; }
        public ProjectWrapperModel(List<Photo> photos, IEnumerable<Projects> ps, Projects p)
        {
            this.p = p;
            this.ps = ps;
            this.photos = photos;
        }
    }
}