using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class GalleryController : Controller
    {
        ImageFileManager imagefilemanager = new ImageFileManager(new EfImageFileDal());
        // GET: Gallery
        public ActionResult Index()
        {
            var values = imagefilemanager.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult ImageAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImageAdd(ImageFile imagefile)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string expansion = Path.GetExtension(Request.Files[0].FileName);
                string path = "/Images/" + filename + expansion;
                Request.Files[0].SaveAs(Server.MapPath(path));
                imagefile.ImagePath = "/Images/" + filename + expansion;
                imagefilemanager.ImageAdd(imagefile);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}