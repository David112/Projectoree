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
    public class ListingsController : Controller
    {
        private ProjectoreeEntities db = new ProjectoreeEntities();

        // GET: Listings
        public ActionResult Index()
        {
            var listings = db.LISTINGS.Include(l => l.PROFILE);
            return View(listings.ToList());
        }

        // POST: Search Results
        [HttpPost]
        public ActionResult Search()
        {
            var id = Request.Form["Search"];

            var listings = db.LISTINGS.Where(db => db.title.Contains(id) || db.description.Contains(id));
            
            return View(listings);
        }
        public ActionResult Search(string id)
        {
            if (id == null)
            {
                id = Request.Form["Search"];
            }

            var listings = db.LISTINGS.Where(db => db.title.Contains(id) || db.description.Contains(id));

            return View(listings);
        }

        // GET: My Projects
        public ActionResult MyProjects()
        {
            var userID = User.Identity.GetUserId();
            var listings = db.LISTINGS.Where(db => db.userid == userID);
            return View(listings.ToList());
        }

        // GET: Listings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTING listings = db.LISTINGS.Find(id);
            if (listings == null)
            {
                return HttpNotFound();
            }
            return View(listings);
        }

        // GET: Listings/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "projectid,userid,title,listingType,seeker,discipline,description,email,contactnumber,subcategory,supervisors,timeframe,startdate,expiredate,mode,location")] LISTING listing)
        {
            if (ModelState.IsValid)
            {
                listing.userid = User.Identity.GetUserId();
                db.LISTINGS.Add(listing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", listing.userid);
            return RedirectToAction("MyProjects", "Listings");
        }

        // GET: Listings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MyProjects", "Listings");
            }
            LISTING listing = db.LISTINGS.Find(id);
            if (listing == null)
            {
                TempData["errorMessage"] = "Listing not found";
                return RedirectToAction("MyProjects", "Listings");
            }
            if (authenticateUser(listing.userid))
            {
                return RedirectToAction("MyProjects");
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", listing.userid);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "projectid,userid,title,listingType,seeker,discipline,description,email,contactnumber,subcategory,supervisors,timeframe,startdate,expiredate,mode,location")] LISTING listing)
        {
            if (authenticateUser(listing.userid))
            {
                return RedirectToAction("MyProjects");
            }
            if (ModelState.IsValid)
            {
                db.Entry(listing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.PROFILES, "userid", "firstname", listing.userid);
            return View(listing);
        }

        // GET: Listings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTING listing = db.LISTINGS.Find(id);
            if(authenticateUser(listing.userid))
            {
                return RedirectToAction("MyProjects");
            }
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LISTING listing = db.LISTINGS.Find(id);
            if (authenticateUser(listing.userid))
            {
                return RedirectToAction("MyProjects");
            }
            db.LISTINGS.Remove(listing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Authenticate that current user is the creator of a listing - true indicates different users
        public bool authenticateUser(string id)
        { 
            if(id != User.Identity.GetUserId())
            {
                return true;
            }

            return false;
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
