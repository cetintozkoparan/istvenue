using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SocialMediaBL
{
    public class SocialMediaManager
    {
        public static List<SocialMedia> GetList()
        {
            using (MainContext db = new MainContext())
            {
                var list = db.SocialMedia.ToList();
                return list;
            }
        }

    }
}
