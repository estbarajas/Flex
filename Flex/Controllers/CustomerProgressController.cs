﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flex.Models;
using Microsoft.AspNet.Identity;

namespace Flex.Controllers
{
    public class CustomerProgressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Bar()
        {
            var userId = User.Identity.GetUserId();
            //var customerProgresses = db.CustomerProgresses.Include(c => c.User);
            var lastProgressEntry = db.CustomerProgresses.OrderByDescending(c => c.Id).Where(c => c.UserId == userId).FirstOrDefault();
            var firstProgressEntry = db.CustomerProgresses.OrderBy(c => c.Id).Where(c => c.UserId == userId).FirstOrDefault();
            //var object1 = lastProgressEntry.las

            var changeOne = Math.Abs(Convert.ToInt32(firstProgressEntry.CurrentWeight) - Convert.ToInt32(lastProgressEntry.WeightGoal));
            var changeTwo = Math.Abs(Convert.ToInt32(firstProgressEntry.CurrentWeight) - Convert.ToInt32(lastProgressEntry.CurrentWeight));
            decimal progressDecimal = (Convert.ToDecimal(changeTwo) / Convert.ToDecimal(changeOne)) * 100;
            int progressInt = Convert.ToInt32(progressDecimal);

            ViewBag.Progress = progressInt;
            ViewBag.PClass = "p" + progressInt;
            ViewBag.WeightStart = firstProgressEntry.CurrentWeight;
            ViewBag.WeightGoal = lastProgressEntry.WeightGoal;

            return View();
        }

        // GET: CustomerProgress
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var customerProgressEntries = db.CustomerProgresses.Where(c => c.UserId == userId).ToList();
            return View(customerProgressEntries);
        }

        // GET: CustomerProgress/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProgress customerProgress = db.CustomerProgresses.Find(id);
            if (customerProgress == null)
            {
                return HttpNotFound();
            }
            return View(customerProgress);
        }

        // GET: CustomerProgress/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: CustomerProgress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CurrentWeight,WeightGoal,UserId")] CustomerProgress customerProgress)
        {

            var userId = User.Identity.GetUserId();
            //var theCustomer = db.Users.Where(c => c.Id == userId).Single();

            if (ModelState.IsValid)
            {
                customerProgress.UserId = userId;
                customerProgress.ProgressDate = DateTime.Today; 
                db.CustomerProgresses.Add(customerProgress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerProgress.UserId);
            return View(customerProgress);
        }

        // GET: CustomerProgress/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProgress customerProgress = db.CustomerProgresses.Find(id);
            if (customerProgress == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerProgress.UserId);
            return View(customerProgress);
        }

        // POST: CustomerProgress/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrentWeight,WeightGoal,UserId")] CustomerProgress customerProgress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProgress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerProgress.UserId);
            return View(customerProgress);
        }

        // GET: CustomerProgress/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProgress customerProgress = db.CustomerProgresses.Find(id);
            if (customerProgress == null)
            {
                return HttpNotFound();
            }
            return View(customerProgress);
        }

        // POST: CustomerProgress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerProgress customerProgress = db.CustomerProgresses.Find(id);
            db.CustomerProgresses.Remove(customerProgress);
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
