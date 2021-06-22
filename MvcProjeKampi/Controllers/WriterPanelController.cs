using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace MvcProjeKampi.Controllers
{

    public class WriterPanelController : Controller
    {
        HeadingManager headingmanager = new HeadingManager(new EfHeadingDal());
        CategoryManager categorymanager = new CategoryManager(new EfCategoryDal());
        WriterManager writermanager = new WriterManager(new EfWriterDal());

        Context c = new Context();

        // GET: WriterPanel

        public ActionResult WriterProfile(int id = 0)
        {
            string p = (string)Session["WriterMail"];
            id = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();
            var writervalue = writermanager.GetByID(id);
            return View();
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator writervalidator = new WriterValidator();

            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                writermanager.WriterUpdate(p);
                return RedirectToAction("AllHeading", "WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterId).FirstOrDefault();
            var contentvalues = headingmanager.GetListByWriter(writeridinfo);
            return View(contentvalues);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            var valuecategory = (from x in categorymanager.GetList()
                                 select new SelectListItem
                                 {
                                     Text = x.CategoryName,
                                     Value = x.CategoryID.ToString()
                                 }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            string m = (string)Session["WriterMail"];
            var writeridinfo = c.Writers.Where(x => x.WriterMail == m).Select(y => y.WriterId).FirstOrDefault();
            ViewBag.d = writeridinfo;
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writeridinfo;
            p.HeadingStatus = true;
            headingmanager.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var valueCategory = (from x in categorymanager.GetList()
                                 select new SelectListItem
                                 {
                                     Text = x.CategoryName,
                                     Value = x.CategoryID.ToString()
                                 }).ToList();
            ViewBag.vlc = valueCategory;

            var HeadingValue = headingmanager.GetByID(id);
            return View(HeadingValue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            headingmanager.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteHeading(int id)
        {

            var result = headingmanager.GetByID(id);

            if (result.HeadingStatus == true)
            {
                result.HeadingStatus = false;
            }
            else
            {
                result.HeadingStatus = true;
            }

            headingmanager.HeadingDelete(result);
            return RedirectToAction("MyHeading");

            //var headingvalue = hm.GetByID(id);
            //headingvalue.HeadingStatus = false;
            //hm.HeadingDelete(headingvalue);
            //return RedirectToAction("Index");
        }
        public ActionResult AllHeading(int sayfa = 1)
        {
            var headings = headingmanager.GetList().ToPagedList(sayfa, 8);
            return View(headings);
        }
    }
}