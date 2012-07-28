using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WFH.Controllers.Common;
using WFH.Models;

namespace WFH.Controllers.Api
{
    [Authorize]
    public class DaysAtHomeController : ApiController
    {
        private readonly IDaysAtHomeLogic logic;

        public DaysAtHomeController(IDaysAtHomeLogic logic)
        {
            this.logic = logic;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            logic.User = User;
        }

        private object DataFormat(DayAtHome d)
        {
            return new
            {
                id = d.ID,
                start = d.Start,
                note = d.Note,
                name = d.Account.Name,
                company = d.Account.Company.Name
            };
        }

        // GET api/DaysAtHome
        public IEnumerable<object> GetDayAtHomes()
        {
            return logic.TodaysItems.Select(DataFormat).AsEnumerable();
        }

        // POST api/DaysAtHome
        public HttpResponseMessage PostDayAtHome(dynamic model)
        {
            var dayathome = new DayAtHome { Note = model.note };
            if (logic.AtHomeToday(ModelState, dayathome))
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, DataFormat(dayathome));
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = dayathome.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DaysAtHome/5
        public HttpResponseMessage DeleteDayAtHome(int id)
        {
            if(logic.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected override void Dispose(bool disposing)
        {
            logic.Dispose();
            base.Dispose(disposing);
        }
    }
}