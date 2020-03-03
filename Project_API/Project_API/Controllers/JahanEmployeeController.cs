using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectAPI.Controllers
{
    public class JahanEmployeeController : ApiController
    {
        ProjectDBEntities context = new ProjectDBEntities();

        [Route("api/Employees")]
        public IHttpActionResult Get()
        {
            var list = context.Employees.ToList();
            List<Employee> empList = new List<Employee>();
            foreach (var item in list)
            {
                Employee emp = new Employee();
                emp.HireDate = item.HireDate;
                emp.Salary = item.Salary;
                emp.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "GET", Rel = "Self" });
                emp.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + emp.Id, Method = "GET", Rel = "Specific Resource" });
                emp.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + emp.Id, Method = "PUT", Rel = "Resource Edit" });
                emp.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + emp.Id, Method = "DELETE", Rel = "Resource Delete" });
                emp.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "POST", Rel = "Resource Create" });
                empList.Add(emp);
            }
            return Ok(empList);
        }

        [Route("api/Employees/{id}", Name = "GetEmployeeById")]
        public IHttpActionResult Get(int id)
        {
            var item = context.Employees.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                Employee log = new Employee();
                log.Id = item.Id;
                log.Salary = item.Salary;
                log.HireDate = item.HireDate;
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "POST", Rel = "Resource Create" });
                return Ok(log);
            }
        }

        [Route("api/Employees")]
        public IHttpActionResult Post(Employee employee)
        {
            Employee log = new Employee();
            context.Employees.Add(employee);
            context.SaveChanges();
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "GET", Rel = "Self" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "GET", Rel = "Specific Resource" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
            log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "POST", Rel = "Resource Create" });
            return Created(Url.Link("GetEmployeeById", new { id = employee.Id }), employee);
        }

        [Route("api/Employees/{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Employee employee)
        {
            var item = context.Employees.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                item.Salary = employee.Salary;

                Employee log = new Employee();
                log.Salary = employee.Salary;
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "POST", Rel = "Resource Create" });
                return Ok(log);
            }
        }

        [Route("api/Employees/{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Employees.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                Employee log = new Employee();
                context.Employees.Remove(item);
                context.SaveChanges();
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "GET", Rel = "Self" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "GET", Rel = "Specific Resource" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "PUT", Rel = "Resource Edit" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees/" + log.Id, Method = "DELETE", Rel = "Resource Delete" });
                log.links.Add(new Links() { HRef = "http://localhost:59819/api/Employees", Method = "POST", Rel = "Resource Create" });
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
