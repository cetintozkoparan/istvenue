using BLL.LinkBL;
using System.Web.Mvc;

namespace web.Controllers
{
    public class FEquipmentController : Controller
    {
        string lang = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

        public ActionResult Index()
        {
            var list = EquipmentManager.GetListForFront(lang);
            return View(list);
        }
    }
}
