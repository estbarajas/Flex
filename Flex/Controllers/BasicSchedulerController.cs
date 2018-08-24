using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Flex.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flex.Controllers
{
    public class BasicSchedulerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Flat;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2016, 5, 5);
            return View(sched);
        }

        public ContentResult Data()
        {
            string currentUser = User.Identity.GetUserId();
            var appointments = db.Events.Where(e => e.UserId == "d5265abc-4aee-4d48-9986-885226ad1ee1").ToList();

            return new SchedulerAjaxData(appointments);

            //var allEvents = from e in db.Events select e;

            //return (new SchedulerAjaxData(
            //    new ApplicationDbContext().Events
            //    .Select(e => new { e.Id, e.Text, e.Start_Date, e.End_Date })
            //    )
            //    );


            //var allEventsList;

            //return new SchedulerAjaxData(allEvents.ToList());
            /////var userId = User.Identity.GetUserId();

            ///// var thisEvent = from s in db.Events
            ///select s;

            //var stylists = db.Stylists.ToList();
            /////string currentUser = User.Identity.GetUserId();
            //var stylistInfo = db.Stylists.Where(c => c.UserId == currentUser).FirstOrDefault();
            /////var myEvent = thisEvent.Where(e => e.UserId == userId);

            /////return new SchedulerAjaxData(myEvent.ToList());






        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
            var entities = new ApplicationDbContext();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        var userId = User.Identity.GetUserId();
                        changedEvent.UserId = userId;
                        entities.Events.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Events.FirstOrDefault(ev => ev.Id == action.SourceId);
                        entities.Events.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.Events.Single(e => e.Id == changedEvent.Id);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                
                entities.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
        }




    }
}