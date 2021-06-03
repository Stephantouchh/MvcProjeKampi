using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context _context = new Context();
        public ActionResult Index()
        {
            var ToplamKategori = _context.Categories.Count(); //Toplam Kategori Sayisi
            ViewBag.ToplamKategoriSayısı = ToplamKategori;

            var YazılımKategorisi = _context.Headings.Count(x => x.CategoryID == 8); // Yazilim Kategorisi id 8, başlik sayisi
            ViewBag.YazılımKategorisisBasNum = YazılımKategorisi;

            var ŞartlıYazarAdı = _context.Writers.Count(x => x.WriterName.Contains("a")); // Yazar adinda "a" harfi gecen yazar sayisi
            ViewBag.ŞartlıYazarAdımız = ŞartlıYazarAdı;

            var EnFazlaBaşlık = _context.Headings.Where(u => u.CategoryID == _context.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count())
                .Select(x => x.Key).FirstOrDefault()).Select(x => x.Category.CategoryName).FirstOrDefault(); // En fazla basliga sahip kategori adi
            ViewBag.EnFazlaBaşlığımız = EnFazlaBaşlık;

            var DurumuTrueOlanKategoriler = _context.Categories.Count(x => x.CategoryStatus == true); // Kategoriler tablosundaki aktif kategori sayisi
            ViewBag.AktifKategoriler = DurumuTrueOlanKategoriler;

            return View();
        }
    }
}