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
    public class CustomerClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerClasses
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //var employee = db.Employees.Include(e => e.Order).Where(c => c.UserId == thisUser).ToList();

            var customerClasses = db.CustomerClasses.Include(c => c.Class).Where(c => c.UserId == userId).ToList();
            return View(customerClasses);
        }

        // GET: CustomerClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerClasses customerClasses = db.CustomerClasses.Find(id);
            if (customerClasses == null)
            {
                return HttpNotFound();
            }
            return View(customerClasses);
        }

        // GET: CustomerClasses/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name");
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: CustomerClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassId,FirstName,LastName,Email,UserId")] CustomerClasses customerClasses)
        {
            var userId = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => c.UserId == userId).Single();

            if (ModelState.IsValid)
            {
                customerClasses.FirstName = customer.FirstName;
                customerClasses.LastName = customer.LastName;
                customerClasses.Email = customer.Email;
                customerClasses.UserId = customer.UserId;
                db.CustomerClasses.Add(customerClasses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", customerClasses.ClassId);
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerClasses.UserId);
            return View(customerClasses);
        }

        // GET: CustomerClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerClasses customerClasses = db.CustomerClasses.Find(id);
            if (customerClasses == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", customerClasses.ClassId);
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerClasses.UserId);
            return View(customerClasses);
        }

        // POST: CustomerClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClassId,FirstName,LastName,Email,UserId")] CustomerClasses customerClasses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerClasses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Name", customerClasses.ClassId);
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customerClasses.UserId);
            return View(customerClasses);
        }

        // GET: CustomerClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerClasses customerClasses = db.CustomerClasses.Find(id);
            if (customerClasses == null)
            {
                return HttpNotFound();
            }
            return View(customerClasses);
        }

        // POST: CustomerClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerClasses customerClasses = db.CustomerClasses.Find(id);
            db.CustomerClasses.Remove(customerClasses);
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
