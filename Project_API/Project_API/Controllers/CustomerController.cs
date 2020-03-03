using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project_API.Models;

namespace Project_API.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomerController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Customer> cList = new List<Customer>();
            var list = context.Customers.ToList();
            if (list == null)
                return StatusCode(HttpStatusCode.NoContent);
            Customer c;
            foreach(Customer item in list)
            {
                c = new Customer();
                c.Id = item.Id;
                c.User_Id = item.User_Id;
                c.Area = item.Area;

                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers", Method = "GET", Rel = "Get all" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + c.Id, Method = "GET", Rel = "Get specific" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + c.Id+"/users", Method = "GET", Rel = "Get user information about a customer" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + c.Id, Method = "POST", Rel = "Add new customer" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + c.Id+"/orders", Method = "GET", Rel = "Get all orders" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + c.Id+"/orders/{orderid}", Method = "GET", Rel = "Get specific order" });

                cList.Add(c);
            }

            return Ok(cList);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var c = context.Customers.Where(cc => cc.Id == id).FirstOrDefault();
            if (c == null)
                return StatusCode(HttpStatusCode.NoContent);
            Customer cus = new Customer();
            cus.Id = c.Id;
            cus.User_Id = c.User_Id;
            cus.Area = c.Area;
            cus.User = null;

            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers", Method = "GET", Rel = "Get all" });
            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + cus.Id, Method = "GET", Rel = "Get specific" });
            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + cus.Id + "/users", Method = "GET", Rel = "Get user information about a customer" });
            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + cus.Id, Method = "POST", Rel = "Add new customer" });
            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + cus.Id + "/orders", Method = "GET", Rel = "Get all orders" });
            cus.links.Add(new Links() { HRef = "http://localhost:56991/api/customers/" + cus.Id + "/orders/{orderid}", Method = "GET", Rel = "Get specific order" });

            return Ok(cus);
        }

        [Route("{id}/users")]
        public IHttpActionResult GetUser(int id)
        {
            var c = context.Customers.Where(cc => cc.Id == id).FirstOrDefault().User;
            if (c == null)
                return StatusCode(HttpStatusCode.NoContent);
            User u = new User();
            u.Id = c.Id;
            u.Name = c.Name;
            u.AddedDate = c.AddedDate;
            u.Address = c.Address;
            u.DOB = c.DOB;

            return Ok(u);
        }

        [Route("")]
        public IHttpActionResult Post(Customer c)
        {
            context.Customers.Add(c);
            context.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }

        [Route("{id}/orders")]
        public IHttpActionResult GetOrders(int id)
        {
            var list = context.Customers.Where(c => c.Id == id).FirstOrDefault().User.Orders.ToList();
            if (list == null || list.Count <1)
                return StatusCode(HttpStatusCode.NoContent);
            List<Order> oList = new List<Order>();
            foreach (var item in list)
            {
                Order order = new Order();
                order.Id = item.Id;
                order.User_Id = item.User_Id;
                order.Name = item.Name;
                order.PhoneNumber = item.PhoneNumber;
                order.Area = item.Area;
                order.Address = item.Address;
                order.Status = item.Address;
                order.ProcessedBy = item.ProcessedBy;
                order.DeliveredBy = item.DeliveredBy;
                order.AddedDate = item.AddedDate;
                order.ModifiedDate = item.ModifiedDate;
                order.Coupon_Id = item.Coupon_Id;

                order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "GET", Rel = "All Order" });
                order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "GET", Rel = "Specific Order" });
                order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "PUT", Rel = "Order Edit" });
                order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "DELETE", Rel = "Order Delete" });
                order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "POST", Rel = "Order Create" });


                oList.Add(order);
            }
            return Ok(oList);
        }

        [Route("{id}/orders/{orderid}")]
        public IHttpActionResult GetOrders(int id, int orderid)
        {
            var item = context.Customers.Where(c => c.Id == id).FirstOrDefault().User.Orders.Where(o => o.Id == orderid).FirstOrDefault();
            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
           
            Order order = new Order();
            order.Id = item.Id;
            order.User_Id = item.User_Id;
            order.Name = item.Name;
            order.PhoneNumber = item.PhoneNumber;
            order.Area = item.Area;
            order.Address = item.Address;
            order.Status = item.Address;
            order.ProcessedBy = item.ProcessedBy;
            order.DeliveredBy = item.DeliveredBy;
            order.AddedDate = item.AddedDate;
            order.ModifiedDate = item.ModifiedDate;
            order.Coupon_Id = item.Coupon_Id;

            order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "GET", Rel = "All Order" });
            order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "GET", Rel = "Specific Order" });
            order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "PUT", Rel = "Order Edit" });
            order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + order.Id, Method = "DELETE", Rel = "Order Delete" });
            order.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "POST", Rel = "Order Create" });


      
            return Ok(order);
        }
    }
}
