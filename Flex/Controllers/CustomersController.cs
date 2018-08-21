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
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult GainCalculator(string Weight, string Height, string Age, string genders, string activityLevel)
        {
            //List<SelectListItem> genders = new List<SelectListItem>();
            //SelectListItem male = new SelectListItem() { Text = "Male", Value = "Male", Selected = true };
            //SelectListItem female = new SelectListItem() { Text = "Female", Value = "Female", Selected = false };
            //genders.Add(male);
            //genders.Add(female);

            //ViewBag.Genders = genders;

            if (!String.IsNullOrEmpty(Weight) && genders.Equals("Male")) //use this for day and bottom code run all time for zipcode
            {
               
                double maleBMR = 66 + (6.2 * Convert.ToDouble(Weight)) + (12.7 * Convert.ToDouble(Height)) - (6.76 * Convert.ToDouble(Age));
                double caloriesToMaintainWeight = maleBMR * Convert.ToDouble(activityLevel);
                double caloriesToGainOnePoundPerWeek = caloriesToMaintainWeight + 500;
                double caloriesToGainTwoPoundsPerWeek = caloriesToGainOnePoundPerWeek + 500;

                ViewBag.caloriesToMaintainWeight = caloriesToMaintainWeight;
                ViewBag.caloriesToGainOnePoundPerWeek = caloriesToGainOnePoundPerWeek;
                ViewBag.caloriesToGainTwoPoundsPerWeek = caloriesToGainTwoPoundsPerWeek;
            }
            else if(!String.IsNullOrEmpty(Weight) && genders.Equals("Female"))
            {
                double femaleBMR = 655.1 + (4.35 * Convert.ToDouble(Weight)) +(4.7 * Convert.ToDouble(Height)) -(4.7 * Convert.ToDouble(Age));
                double caloriesToMaintainWeight = femaleBMR * Convert.ToDouble(activityLevel);
                double caloriesToGainOnePoundPerWeek = caloriesToMaintainWeight + 500;
                double caloriesToGainTwoPoundsPerWeek = caloriesToGainOnePoundPerWeek + 500;


                //ViewBag.BMR = femaleBMR;
                ViewBag.caloriesToMaintainWeight = caloriesToMaintainWeight;
                ViewBag.caloriesToGainOnePoundPerWeek = caloriesToGainOnePoundPerWeek;
                ViewBag.caloriesToGainTwoPoundsPerWeek = caloriesToGainTwoPoundsPerWeek;
            }

            return View();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.User);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,MembershipId,UserId")] Customer customer)
        {
            var userId = User.Identity.GetUserId();
            var theCustomer = db.Users.Where(c => c.Id == userId).Single();

            if (ModelState.IsValid)
            {
                customer.Email = theCustomer.Email;
                customer.UserId = User.Identity.GetUserId();
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customer.UserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,MembershipId,UserId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
