using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingValidator hv = new HeadingValidator();
        public ActionResult Index()
        {
            var headingvalues = hm.GetList();
            return View(headingvalues);
        }
        [HttpGet]
        public ActionResult AddHeading()
        {
            var valuecategory = (from x in cm.GetList()
                                 select new SelectListItem
                                 {
                                     Text = x.CategoryName,
                                     Value = x.CategoryID.ToString()
                                 }).ToList();
            ViewBag.vlc = valuecategory;

            var valuewriter = (from x in wm.GetList()
                               select new SelectListItem
                               {
                                   Text = x.WriterName + " " + x.WriterSurName,
                                   Value = x.WriterId.ToString()
                               }).ToList();
            ViewBag.wlc = valuewriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            ValidationResult results = hv.Validate(p);
            if (results.IsValid)
            {
                hm.HeadingAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();

            //p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            //hm.HeadingAdd(p);
            //return RedirectToAction("Index");
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

            ValidationResult validationResult = hv.Validate(p);
            if (validationResult.IsValid)
            {
                p.HeadingStatus = true;
                hm.HeadingUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();

            //hm.HeadingUpdate(p);
            //return RedirectToAction("Index");
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
            return RedirectToAction("Index");

            //var headingvalue = hm.GetByID(id);
            //headingvalue.HeadingStatus = false;
            //hm.HeadingDelete(headingvalue);
            //return RedirectToAction("Index");
        }
    }
}