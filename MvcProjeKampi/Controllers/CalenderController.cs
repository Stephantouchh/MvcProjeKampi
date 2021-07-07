using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MvcProjeKampi.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class CalenderController : Controller
    {
        HeadingManager headingmanager = new HeadingManager(new EfHeadingDal());
        // GET: Calender
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Calender());
        }
        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var viewModel = new Calender();
            var events = new List<Calender>();
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-14);

            foreach (var item in headingmanager.GetList())
            {
                events.Add(new Calender()
                {
                    Title = item.HeadingName,
                    Start = item.HeadingDate,
                    End = item.HeadingDate.AddDays(-14),
                    AllDay = false
                });

                start = start.AddDays(7);
                end = end.AddDays(7);
            }


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}