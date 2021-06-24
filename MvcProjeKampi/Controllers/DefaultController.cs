using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default
        HeadingManager headingmanager = new HeadingManager(new EfHeadingDal());
        ContentManager contentmanager = new ContentManager(new EfContentDal());
        public ActionResult Headings()
        {
            var headinglist = headingmanager.GetList();
            return View(headinglist);
        }

        public PartialViewResult Index(int id = 4)
        {
            var contentlist = contentmanager.GetListByHeadingID(id);
            return PartialView(contentlist);
        }
    }
}