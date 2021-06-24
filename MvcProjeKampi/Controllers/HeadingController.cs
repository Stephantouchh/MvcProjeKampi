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

        HeadingManager headingmanager = new HeadingManager(new EfHeadingDal());
        CategoryManager categorymanager = new CategoryManager(new EfCategoryDal());
        WriterManager writeramanager = new WriterManager(new EfWriterDal());
        HeadingValidator headingvalidator = new HeadingValidator();
        public ActionResult Index()
        {
            var headingvalues = headingmanager.GetList();
            return View(headingvalues);
        }
        [HttpGet]
        public ActionResult AddHeading()
        {
            var valuecategory = (from x in categorymanager.GetList()
                                 select new SelectListItem
                                 {
                                     Text = x.CategoryName,
                                     Value = x.CategoryID.ToString()
                                 }).ToList();
            ViewBag.vlc = valuecategory;

            var valuewriter = (from x in writeramanager.GetList()
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
            ValidationResult results = headingvalidator.Validate(p);
            if (results.IsValid)
            {
                headingmanager.HeadingAdd(p);
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

            ValidationResult validationResult = headingvalidator.Validate(p);
            if (validationResult.IsValid)
            {
                p.HeadingStatus = true;
                headingmanager.HeadingUpdate(p);
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
            return RedirectToAction("Index");

            //var headingvalue = hm.GetByID(id);
            //headingvalue.HeadingStatus = false;
            //hm.HeadingDelete(headingvalue);
            //return RedirectToAction("Index");
        }
    }
}