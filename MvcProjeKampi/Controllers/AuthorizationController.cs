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
    public class AuthorizationController : Controller
    {
        AdminManager adminmanager = new AdminManager(new EfAdminDal());
        RoleManager roleManager = new RoleManager(new EfRoleDal());
        Context _context = new Context();
        // GET: Authorization
        public ActionResult Index()
        {
            var adminvalues = adminmanager.GetList();
            return View(adminvalues);
        }
        //[HttpGet]
        //public ActionResult AddAdmin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddAdmin(Admin admin)    "Buralar RegisterController'de Yapıldı."
        //{
        //    adminmanager.AdminAdd(admin);
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            List<SelectListItem> valueadminrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valueadmin = valueadminrole;

            var adminvalue = adminmanager.GetById(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            admin.AdminStatus = true;
            adminmanager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
    }
}