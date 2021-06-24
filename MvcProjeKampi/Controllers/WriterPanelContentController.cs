using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager contentmanager = new ContentManager(new EfContentDal());
        Context _context = new Context();
        // GET: WriterPanelContent
        public ActionResult MyContent(string p)
        {
            p = (string)Session["WriterMail"];
            var writeridinfo = _context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();
            var contentvalues = contentmanager.GetListByWriter(writeridinfo);
            return View(contentvalues);
        }
        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content p)
        {
            string mail = (string)Session["WriterMail"];
            var writeridinfo = _context.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterId).FirstOrDefault();
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writeridinfo;
            p.ContentStatus = true;
            contentmanager.ContentAdd(p);
            return RedirectToAction("MyContent");
        }
        public ActionResult ToDoList()
        {
            return View();
        }
    }
}