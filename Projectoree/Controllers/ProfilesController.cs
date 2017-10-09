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
    public class ProfilesController : Controller
    {
        private ProjectoreeEntities db = new ProjectoreeEntities();

        // GET: Profiles
        public ActionResult Index()
        {
            return View(db.PROFILES.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFILE pROFILE = db.PROFILES.Find(id);
            if (pROFILE == null)
            {
                return HttpNotFound();
            }
            return View(pROFILE);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,firstname,lastname,email,discipline,contactnumber,skills,units,interests,bio")] PROFILE pROFILE)
        {
            if (ModelState.IsValid)
            {
                db.PROFILES.Add(pROFILE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pROFILE);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFILE pROFILE = db.PROFILES.Find(id);
            if (pROFILE == null)
            {
                return HttpNotFound();
            }
            return View(pROFILE);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,firstname,lastname,email,discipline,contactnumber,skills,units,interests,bio")] PROFILE pROFILE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pROFILE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pROFILE);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFILE pROFILE = db.PROFILES.Find(id);
            if (pROFILE == null)
            {
                return HttpNotFound();
            }
            return View(pROFILE);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PROFILE pROFILE = db.PROFILES.Find(id);
            db.PROFILES.Remove(pROFILE);
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
