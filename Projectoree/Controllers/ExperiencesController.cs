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
            var eXPERIENCEs = db.EXPERIENCEs.Include(e => e.PROFILE);
            return View(eXPERIENCEs.ToList());
        }

        // GET: Experiences/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE eXPERIENCE = db.EXPERIENCEs.Find(id);
            if (eXPERIENCE == null)
            {
                return HttpNotFound();
            }
            return View(eXPERIENCE);
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
        public ActionResult Create([Bind(Include = "experienceid,userid,experience1")] EXPERIENCE eXPERIENCE)
        {
            if (ModelState.IsValid)
            {
                db.EXPERIENCEs.Add(eXPERIENCE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", eXPERIENCE.userid);
            return View(eXPERIENCE);
        }

        // GET: Experiences/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE eXPERIENCE = db.EXPERIENCEs.Find(id);
            if (eXPERIENCE == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", eXPERIENCE.userid);
            return View(eXPERIENCE);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "experienceid,userid,experience1")] EXPERIENCE eXPERIENCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXPERIENCE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", eXPERIENCE.userid);
            return View(eXPERIENCE);
        }

        // GET: Experiences/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCE eXPERIENCE = db.EXPERIENCEs.Find(id);
            if (eXPERIENCE == null)
            {
                return HttpNotFound();
            }
            return View(eXPERIENCE);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            EXPERIENCE eXPERIENCE = db.EXPERIENCEs.Find(id);
            db.EXPERIENCEs.Remove(eXPERIENCE);
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
