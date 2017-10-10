using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projectoree.Models;

namespace Projectoree.Controllers
{
    public class ExperiencesController : Controller
    {
        private ProjectoreeEntities db = new ProjectoreeEntities();

        // GET: Experiences
        public ActionResult Index()
        {
            var experiences = db.EXPERIENCEs.Include(e => e.PROFILE);
            return View(experiences.ToList());
        }

        // GET: Experiences/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE experience = db.EXPERIENCEs.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname");
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,experience1")] EXPERIENCE experience)
        {
            if (ModelState.IsValid)
            {
                db.EXPERIENCEs.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", experience.userid);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE experience = db.EXPERIENCEs.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", experience.userid);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,experience1")] EXPERIENCE experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", experience.userid);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE experience = db.EXPERIENCEs.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EXPERIENCE experience = db.EXPERIENCEs.Find(id);
            db.EXPERIENCEs.Remove(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
