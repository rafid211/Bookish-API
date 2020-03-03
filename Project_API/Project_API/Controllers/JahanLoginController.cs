using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectAPI.Controllers
{
    public class JahanLoginController : ApiController
    {
        ProjectDBEntities context = new ProjectDBEntities();

        // GET: api/Logins
        [Route("api/Logins")]
        public IHttpActionResult Get()
        {
            var list = context.Logins.ToList();
            List<Login> loginList = new List<Login>();
            foreach (var item in list)
            {
                Login log = new Login();
                log.Id = item.Id;
                log.Email = item.Email;
                log.Status = item.Status;
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "POST", Rel = "Resource Create" });
                loginList.Add(log);
            }
            return Ok(loginList);
        }

        // GET: api/Logins/5
        [Route("api/Logins/{id}", Name = "GetLoginById")]
        public IHttpActionResult Get(int id)
        {
            var item = context.Logins.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                Login log = new Login();
                log.Id = item.Id;
                log.Email = item.Email;
                log.Status = item.Status;
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "POST", Rel = "Resource Create" });              
                return Ok(log);
            }
        }

        // POST: api/Logins
        [Route("api/Logins")]
        public IHttpActionResult Post(Login login)
        {
            Login log = new Login();
            context.Logins.Add(login);
            context.SaveChanges();
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "GET", Rel = "Self" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "GET", Rel = "Specific Resource" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "POST", Rel = "Resource Create" });
            return Created(Url.Link("GetLoginById", new { id = login.Id }), login);
        }

        // PUT: api/Logins/5
        [Route("api/Logins/{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Login login)
        {
            var item = context.Logins.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                item.Email = login.Email;
                item.Status = login.Status;

                Login log = new Login();
                log.Email = login.Email;
                log.Status = login.Status;
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "POST", Rel = "Resource Create" });
                return Ok(log);
            }
        }

        // DELETE: api/Logins/5
        [Route("api/Logins/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Logins.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                Login log = new Login();
                context.Logins.Remove(item);
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Logins", Method = "POST", Rel = "Resource Create" });
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
