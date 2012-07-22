using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WFH.Models;

namespace WFH.Controllers
{
    public class DaysAtHomeController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var tomorrow =  DateTime.Today.AddDays(1);
            var todaysItems = db.DaysAtHome
                                .Where(d => d.Start >= DateTime.Today)
                                .Where(d => d.Start < tomorrow);

            return View(todaysItems);
        }

        [HttpPost]
        public ActionResult AtHomeToday(DayAtHome dayAtHome)
        {
            var user = Membership.GetUser(User.Identity.Name, userIsOnline: true);
            var account = db.Accounts.Single(a => a.UserID == (Guid)user.ProviderUserKey);

            dayAtHome.Account = account;
            dayAtHome.Start = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.DaysAtHome.Add(dayAtHome);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DayAtHome dayathome = db.DaysAtHome.Find(id);
            db.DaysAtHome.Remove(dayathome);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}