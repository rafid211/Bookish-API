using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_API.Models;

namespace Project_API.Controllers
{
    [RoutePrefix("api/Reports")]
    public class ReportController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();
        private ProjectDBEntities context = new ProjectDBEntities();

        public ReportController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Reports.ToList();
            List<Report> reports = new List<Report>();
            foreach (var item in list)
            {
                Report obj = new Report();
                obj.Book_Id = item.Book_Id;
                obj.Id = item.Id;
                obj.Detail = item.Detail;
                obj.Status = item.Status;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports", Method = "POST", Rel = "Resource Create" });



                reports.Add(obj);
            }
            return Ok(reports);
        }

        [Route("{id}", Name = "GetReportsById")]
        [ResponseType(typeof(Report))]
        public IHttpActionResult Get([FromUri]int id)
        {
            Report obj = db.Reports.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports", Method = "GET", Rel = "All Resource" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Reports", Method = "POST", Rel = "Resource Create" });


            return Ok(obj);
        }

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReport(int id, Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != report.Id)
            {
                return BadRequest();
            }

            db.Entry(report).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Report
        [ResponseType(typeof(Report))]
        public IHttpActionResult PostReport(Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reports.Add(report);
            db.SaveChanges();

            return CreatedAtRoute("GetReportsById", new { id = report.Id }, report);
        }

        [Route("{id}")]
        [ResponseType(typeof(Report))]
        public IHttpActionResult DeleteReport(int id)
        {
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            db.Reports.Remove(report);
            db.SaveChanges();

            return Ok(report);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportExists(int id)
        {
            return db.Reports.Count(e => e.Id == id) > 0;
        }
    }
}