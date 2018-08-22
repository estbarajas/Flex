using System;
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
            //var customerProgresses = db.CustomerProgresses.Include(c => c.User);
            return View();
        }

        // GET: CustomerProgress
        public ActionResult Index()
        {
            var customerProgresses = db.CustomerProgresses.Include(c => c.User);
            return View(customerProgresses.ToList());
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
