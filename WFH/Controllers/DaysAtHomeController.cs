using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WFH.Controllers.Common;
using WFH.Models;

namespace WFH.Controllers
{
    public class DaysAtHomeController : Controller
    {
        private IDaysAtHomeLogic logic;

        public DaysAtHomeController(IDaysAtHomeLogic logic)
        {
            this.logic = logic;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            logic.User = User;
        }

        public ActionResult Index()
        {
            if (!logic.IsAuthorized) return RedirectToAction("GetStarted");

            ViewBag.AuthenticationID = logic.AuthenticationID;

            return View(logic.TodaysItems);
        }

        public ActionResult GetStarted()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AtHomeToday(DayAtHome dayAtHome)
        {
            logic.AtHomeToday(ModelState, dayAtHome);

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            logic.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            logic.Dispose();
            base.Dispose(disposing);
        }

    }
}