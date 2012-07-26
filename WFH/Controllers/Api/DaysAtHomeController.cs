using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using WFH.Models;

namespace WFH.Controllers.Api
{
    public class DaysAtHomeController : ApiController
    {
        private AppContext db = new AppContext();

        // GET api/DaysAtHome
        public IEnumerable<object> GetDayAtHomes()
        {
            return db.DaysAtHome.Select(d => new
            {
                id = d.ID,
                start = d.Start,
                note = d.Note,
                name = d.Account.Name,
                company = d.Account.Company.Name
            }).AsEnumerable();
        }

        // GET api/DaysAtHome/5
        public DayAtHome GetDayAtHome(int id)
        {
            DayAtHome dayathome = db.DaysAtHome.Find(id);
            if (dayathome == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return dayathome;
        }

        // PUT api/DaysAtHome/5
        public HttpResponseMessage PutDayAtHome(int id, DayAtHome dayathome)
        {
            if (ModelState.IsValid && id == dayathome.ID)
            {
                db.Entry(dayathome).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, dayathome);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/DaysAtHome
        public HttpResponseMessage PostDayAtHome(DayAtHome dayathome)
        {
            if (ModelState.IsValid)
            {
                db.DaysAtHome.Add(dayathome);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dayathome);
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
            DayAtHome dayathome = db.DaysAtHome.Find(id);
            if (dayathome == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DaysAtHome.Remove(dayathome);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dayathome);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}