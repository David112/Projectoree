﻿using System;
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
            if (Request.Form["Search"] != null)
            {
                var phrase = Request.Form["Search"];
                var type = Request.Form["SearchType"];
                bool seeker;

                if (type == "Projects" || type == null)
                {
                    ViewBag.SearchType = false;
                    seeker = false;
                }
                else
                {
                    ViewBag.SearchType =  true;
                    seeker = true;
                }

                ViewBag.SearchPhrase = phrase;
                var listings = db.LISTINGS.Where(db => db.title.Contains(phrase) || db.description.Contains(phrase)).Where(db => db.seeker == seeker);

                return View(listings);

            }
            else
            {
                ViewBag.SearchType = false;
                var listings = db.LISTINGS.Include(l => l.PROFILE).Where(db => db.seeker == false);
                if (listings.ToList().Count > 0)
                {
                    TempData["last"] = listings.ToList().Last();
                }
                return View(listings.ToList());
            }
        }

        // GET: My Projects
        public ActionResult MyProjects()
        {
            var userID = User.Identity.GetUserId();
            if (userID == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var listings = db.LISTINGS.Where(db => db.userid == userID);
            if (listings.ToList().Count > 0)
            {
                TempData["last"] = listings.ToList().Last();
            }

            return View(listings.ToList());
        }

        // GET: Listings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Manage");
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
            if (User.Identity.GetUserId() == null)
            {
                TempData["errorMessage"] = "Please register to post a project";
                return RedirectToAction("Register", "Account");
            }
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
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("MyProjects");
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
