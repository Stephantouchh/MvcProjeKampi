using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        // GET: WriterPanel
        public ActionResult WriterProfile()
        {
            return View();
        }
        public ActionResult MyHeading()
        {
            //id = 4;
            var values = hm.GetListByWriter();
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            var valuecategory = (from x in cm.GetList()
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
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = 4;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            var valueCategory = (from x in cm.GetList()
                                 select new SelectListItem
                                 {
                                     Text = x.CategoryName,
                                     Value = x.CategoryID.ToString()
                                 }).ToList();
            ViewBag.vlc = valueCategory;

            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteHeading(int id)
        {

            var result = hm.GetByID(id);

            if (result.HeadingStatus == true)
            {
                result.HeadingStatus = false;
            }
            else
            {
                result.HeadingStatus = true;
            }

            hm.HeadingDelete(result);
            return RedirectToAction("MyHeading");

            //var headingvalue = hm.GetByID(id);
            //headingvalue.HeadingStatus = false;
            //hm.HeadingDelete(headingvalue);
            //return RedirectToAction("Index");
        }
    }
}