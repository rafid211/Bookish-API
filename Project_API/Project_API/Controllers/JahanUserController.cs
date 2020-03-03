using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectAPI.Controllers
{
    public class JahanUserController : ApiController
    {
        ProjectDBEntities context = new ProjectDBEntities();

        // GET: api/JahanUser
        [Route("api/Users")]
        public IHttpActionResult Get()
        {
            var list = context.Users.ToList();
            List<User> userList = new List<User>();
            foreach (var item in list)
            {
                User log = new User();
                log.Id = item.Id;
                log.Email = item.Email;
                log.Name = item.Name;
                log.Address = item.Address;
                log.PhoneNumber = log.PhoneNumber;
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "POST", Rel = "Resource Create" });
                userList.Add(log);
            }
            return Ok(userList);
        }

        // GET: api/JahanUser/5
        [Route("api/Users/{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id)
        {
            var item = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                User log = new User();
                log.Id = item.Id;
                log.Email = item.Email;
                log.Name = item.Name;
                log.Address = item.Address;
                log.PhoneNumber = log.PhoneNumber;
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "POST", Rel = "Resource Create" });
                return Ok(log);
            }
        }

        // POST: api/JahanUser
        [Route("api/Users")]
        public IHttpActionResult Post(User user)
        {
            User log = new User();
            context.Users.Add(user);
            context.SaveChanges();
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "GET", Rel = "Self" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "GET", Rel = "Specific Resource" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "POST", Rel = "Resource Create" });
            return Created(Url.Link("GetUserById", new { id = user.Id }), user);
        }

        // PUT: api/JahanUser/5
        [Route("api/Users/{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]User user)
        {
            var item = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                item.Email = user.Email;
                item.Name = user.Name;
                item.PhoneNumber = user.PhoneNumber;
                item.Address = user.Address;

                User log = new User();
                log.Email = user.Email;
                log.Name = user.Name;
                log.PhoneNumber = user.PhoneNumber;
                log.Address = user.Address;
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "POST", Rel = "Resource Create" });
                return Ok(log);
            }
        }

        // DELETE: api/JahanUser/5
        [Route("api/Users/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Users.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                User log = new User();
                context.Users.Remove(item);
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Users", Method = "POST", Rel = "Resource Create" });
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
