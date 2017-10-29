using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projectoree.Models;

namespace Projectoree.Controllers
{
    public class ProfilesController : Controller
    {
        private ProjectoreeEntities db = new ProjectoreeEntities();

        // GET: Profiles
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("MyProjects", "Listings");
            }
            //return View(db.PROFILES.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
            }
            PROFILE Profile = db.PROFILES.Find(id);
            if (Profile == null)
            {
                return RedirectToAction("Index", "Manage");
            }
            return View(Profile);
        }

        //GET: Profiles/Create
        public ActionResult Create()
        {
            if (TempData["profile"] != null)
            {
                Create((PROFILE)TempData["profile"]);
                return View();
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,firstname,lastname,email,discipline,contactnumber,skills,units,interests,bio")] PROFILE Profile)
        {
            if (ModelState.IsValid)
            {
                Profile.userid = User.Identity.GetUserId();
                db.PROFILES.Add(Profile);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Manage");
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
                    //new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROFILE Profile = db.PROFILES.Find(id);
            if (Profile == null || Profile.userid != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Manage");
            }
            return View(Profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userid,firstname,lastname,email,discipline,contactnumber,skills,units,interests,bio")] PROFILE Profile)
        {

            if (Profile.userid != User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Manage");
            }
            if (ModelState.IsValid)
            {
                Profile.email = User.Identity.GetUserName();
                db.Entry(Profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
            return View(Profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                id = User.Identity.GetUserId();
                if (id == null)
                {
                    return RedirectToAction("Index", "Manage");
                }
            }

            PROFILE Profile = db.PROFILES.Find(id);
            
            return View(Profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            var id = User.Identity.GetUserId();
            PROFILE Profile = db.PROFILES.Find(id);
            db.PROFILES.Remove(Profile);
            db.SaveChanges();

            return RedirectToAction("DeleteUser", "Account", new { id });
        }
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                id = User.Identity.GetUserId();
            }
            PROFILE Profile = db.PROFILES.Find(id);
            db.PROFILES.Remove(Profile);
            db.SaveChanges();

            return RedirectToAction("DeleteUser", "Account", new { id });
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
