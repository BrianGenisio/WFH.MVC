using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using WFH.Models;

namespace WFH.Controllers.Common
{
    public interface IDaysAtHomeLogic : IDisposable
    {
        IPrincipal User { get; set; }
        bool IsAuthorized { get; }
        IQueryable<DayAtHome> TodaysItems { get; }
        IQueryable<DayAtHome> All { get; }
        Guid AuthenticationID { get; }
        bool AtHomeToday(dynamic modelState, DayAtHome dayAtHome);
        bool Delete(int id);

    }

    public class DaysAtHomeCommon : IDaysAtHomeLogic
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

        public bool IsAuthorized
        {
            get
            {
                return Account != null;
            }
        }

        public IQueryable<DayAtHome> All
        {
            get
            {
                return db.DaysAtHome;
            }
        }

        public IQueryable<DayAtHome> TodaysItems
        {
            get
            {
                var tomorrow = DateTime.Today.AddDays(1);
                return db.DaysAtHome
                        .Where(d => d.Account.Company.ID == Account.Company.ID)
                        .Where(d => d.Start >= DateTime.Today)
                        .Where(d => d.Start < tomorrow);
            }
        }

        public Guid AuthenticationID
        {
            get
            {
                return AuthenticatedUser != null ?
                     (Guid)AuthenticatedUser.ProviderUserKey :
                     Guid.Empty;
            }
        }

        public bool AtHomeToday(dynamic modelState, DayAtHome dayAtHome)
        {
            dayAtHome.Account = Account;
            dayAtHome.Start = DateTime.Now;

            if (modelState.IsValid)
            {
                db.DaysAtHome.Add(dayAtHome);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            db.Dispose();
        }


        public bool Delete(int id)
        {
            var dayathome =  db.DaysAtHome.Find(id);
            if (dayathome == null)
            {
                return false;
            }

            db.DaysAtHome.Remove(dayathome);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public IPrincipal User { get; set; }
    }
}