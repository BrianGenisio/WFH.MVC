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

        private MembershipUser AuthenticatedUser
        {
            get
            {
                return Membership.GetUser(User.Identity.Name, userIsOnline: true);
            }
        }

        private Account Account
        {
            get
            {
                if (AuthenticatedUser == null) return null;

                return db.Accounts.Single(a => a.UserID == (Guid)AuthenticatedUser.ProviderUserKey);
            }
        }

        public ActionResult Index()
        {
            if (Account == null) return RedirectToAction("GetStarted");
            
            var tomorrow = DateTime.Today.AddDays(1);
            var todaysItems = db.DaysAtHome
                            .Where(d => d.Account.Company.ID == Account.Company.ID)
                            .Where(d => d.Start >= DateTime.Today)
                            .Where(d => d.Start < tomorrow);
        
            ViewBag.AuthenticationID = AuthenticatedUser != null ?
                (Guid)AuthenticatedUser.ProviderUserKey :
                Guid.Empty;

            return View(todaysItems);
        }

        public ActionResult GetStarted()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AtHomeToday(DayAtHome dayAtHome)
        {
            dayAtHome.Account = Account;
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