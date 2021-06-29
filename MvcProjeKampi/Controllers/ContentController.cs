using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager contentmanager = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllContent(string p)
        {
            var values = contentmanager.GetList(p);
            //var values = c.Contents.ToList();
            return View(values);
        }
        public ActionResult ContentByHeading(int id)
        {
            var contentvalues = contentmanager.GetListByHeadingID(id);
            return View(contentvalues);
        }
    }
}