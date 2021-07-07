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
    public class TalentController : Controller
    {
        // GET: Talent
        TalentManager talentManager = new TalentManager(new EfTalentDal());
        public ActionResult Index()
        {
            var result = talentManager.GetTalents();
            return View(result);
        }
        [HttpGet]
        public ActionResult AddTalent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTalent(Talent talent)
        {
            talentManager.Insert(talent);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditTalent(int id)
        {
            var result = talentManager.GetById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult EditTalent(Talent talent)
        {
            talentManager.Update(talent);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTalent(int Id)
        {
            var result = talentManager.GetById(Id);
            talentManager.Delete(result);
            return RedirectToAction("Index");
        }

    }
}