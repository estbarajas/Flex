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
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Flex.Controllers
{
    public class TrainerBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrainerBookings
        public ActionResult Index()
        {
            var trainerBookings = db.TrainerBookings.Include(t => t.User);
            return View(trainerBookings.ToList());
        }

        public ActionResult AcceptedBookings()
        {
            var trainerBookings = db.TrainerBookings.Where(t => t.AcceptSession == true);
            return View(trainerBookings.ToList());
        }

        // GET: TrainerBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerBooking trainerBooking = db.TrainerBookings.Find(id);
            if (trainerBooking == null)
            {
                return HttpNotFound();
            }
            return View(trainerBooking);
        }

        // GET: TrainerBookings/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: TrainerBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Time,AcceptSession,UserId")] TrainerBooking trainerBooking)
        {
            if (ModelState.IsValid)
            {
                trainerBooking.UserId = User.Identity.GetUserId();
                db.TrainerBookings.Add(trainerBooking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", trainerBooking.UserId);
            return View(trainerBooking);
        }

        public void SMSSent()
        {
            if (true)
            {
                // Find your Account Sid and Token at twilio.com/console
                const string accountSid = "ACdd909d4bbac8a3c1a991e04b9b1c8561";
                const string authToken = "2fe53ada44d9b861257521cbe7e787a4";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Your appointment has been accepted!",
                    from: new Twilio.Types.PhoneNumber("+13343527132"),
                    to: new Twilio.Types.PhoneNumber("+14142421061")
                );

                Console.WriteLine(message.Sid);
                //Console.ReadLine();

            };

            //return View();            
        }

        // GET: TrainerBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerBooking trainerBooking = db.TrainerBookings.Find(id);
            if (trainerBooking == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", trainerBooking.UserId);
            return View(trainerBooking);
        }

        // POST: TrainerBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Time,Notes,AcceptSession,UserId")] TrainerBooking trainerBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerBooking).State = EntityState.Modified;
                db.SaveChanges();
                SMSSent();
                return RedirectToAction("Index");
            }
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", trainerBooking.UserId);
            return View(trainerBooking);
        }

        // GET: TrainerBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerBooking trainerBooking = db.TrainerBookings.Find(id);
            if (trainerBooking == null)
            {
                return HttpNotFound();
            }
            return View(trainerBooking);
        }

        // POST: TrainerBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerBooking trainerBooking = db.TrainerBookings.Find(id);
            db.TrainerBookings.Remove(trainerBooking);
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
