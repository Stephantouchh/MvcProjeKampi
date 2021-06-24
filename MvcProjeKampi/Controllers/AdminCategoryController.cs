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
    public class AdminCategoryController : Controller
    {
        // GET: AdminCategory
        CategoryManager categorymanager = new CategoryManager(new EfCategoryDal());


        public ActionResult Index()
        {
            var categoryvalues = categorymanager.GetList();
            return View(categoryvalues);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            CategoryValidator categoryvalidator = new CategoryValidator();
            ValidationResult results = categoryvalidator.Validate(p);
            if (results.IsValid)
            {
                categorymanager.CategoryAdd(p);
                return RedirectToAction("Index");
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

        public ActionResult DeleteCategory(int id)
        {
            var categoryvalue = categorymanager.GetByID(id);
            categorymanager.CategoryDelete(categoryvalue);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var Categoryvalue = categorymanager.GetByID(id);
            return View(Categoryvalue);
        }
        [HttpPost]
        public ActionResult EditCategory(Category p)
        {
            categorymanager.CategoryUpdate(p);
            return RedirectToAction("Index");
        }
    }
}