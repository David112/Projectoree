using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projectoree.Models;

namespace Projectoree.Controllers
{
    public class HomeController : Controller
    {
        private ProjectoreeEntities db = new ProjectoreeEntities();

        public ActionResult Index()
        {
            var listings = db.LISTINGS.OrderBy(db => db.projectid).Take(6);
            ViewBag.page = "Home/Index";
            return View(listings);
        }

        public ActionResult Pause()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}