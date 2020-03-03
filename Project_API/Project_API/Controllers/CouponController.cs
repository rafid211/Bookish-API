using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project_API.Models;

namespace Project_API.Controllers
{
    [RoutePrefix("api/coupons")]
    public class CouponController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Coupon> cLIst = new List<Coupon>();
            var list = context.Coupons.ToList();
            Coupon c;
            if (list == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {
                foreach (Coupon item in list)
                {
                    c = new Coupon();
                    c.Id = item.Id;
                    c.CouponKeyword = item.CouponKeyword;
                    c.ExpireDate = item.ExpireDate;
                    c.Percentage = item.Percentage;
                    c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "GET", Rel = "Get all the coupons" });
                    c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "GET", Rel = "Specific coupon" });
                    c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "PUT", Rel = "Edit Specific coupon" });
                    c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "DELETE", Rel = "Delete Specific coupon" });
                    c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "POST", Rel = "Create a coupon" });
                    cLIst.Add(c);
                }
                return Ok(cLIst);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Coupon cc = new Coupon();
            var item = context.Coupons.Where(c => c.Id == id).FirstOrDefault();

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                cc = new Coupon();
                cc.Id = item.Id;
                cc.CouponKeyword = item.CouponKeyword;
                cc.ExpireDate = item.ExpireDate;
                cc.Percentage = item.Percentage;
                cc.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "GET", Rel = "Get all the coupons" });
                cc.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "GET", Rel = "Specific coupon" });
                cc.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "PUT", Rel = "Edit Specific coupon" });
                cc.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "DELETE", Rel = "Delete Specific coupon" });
                cc.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "POST", Rel = "Create a coupon" });

                return Ok(cc);
            }

        }

        [Route("{id}")]
        public IHttpActionResult PUT([FromUri]int id, [FromBody]Coupon cb)
        {

            var item = context.Coupons.Where(c => c.Id == id).FirstOrDefault();

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                item.CouponKeyword = cb.CouponKeyword;
                item.ExpireDate = cb.ExpireDate;
                item.Percentage = cb.Percentage;

                item.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "GET", Rel = "Get all the coupons" });
                item.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "GET", Rel = "Specific coupon" });
                item.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "PUT", Rel = "Edit Specific coupon" });
                item.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + item.Id, Method = "DELETE", Rel = "Delete Specific coupon" });
                item.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "POST", Rel = "Create a coupon" });
                context.SaveChanges();

                return Ok(item);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {

            var item = context.Coupons.Where(c => c.Id == id).FirstOrDefault();

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                context.Coupons.Remove(item);
                context.SaveChanges();

                return StatusCode(HttpStatusCode.NoContent);
            }

        }

        [Route("")]
        public IHttpActionResult Post(Coupon c)
        {

            if (c == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                context.Coupons.Add(c);
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "GET", Rel = "Get all the coupons" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + c.Id, Method = "GET", Rel = "Specific coupon" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + c.Id, Method = "PUT", Rel = "Edit Specific coupon" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons/" + c.Id, Method = "DELETE", Rel = "Delete Specific coupon" });
                c.links.Add(new Links() { HRef = "http://localhost:56991/api/coupons", Method = "POST", Rel = "Create a coupon" });
                context.SaveChanges();


                return Ok(c);
            }

        }

        [Route("{id}/orders")]
        public IHttpActionResult GetOrders(int id)
        {
            Order o;
            var item = context.Coupons.Where(c => c.Id == id).FirstOrDefault();

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {
                List<Order> oList = new List<Order>();
                foreach (Order oo in item.Orders)
                {
                    o = new Order();
                    o.Id = oo.Id;
                    o.User_Id = oo.User_Id;
                    o.Name = oo.Name;
                    o.PhoneNumber = oo.PhoneNumber;
                    o.ProcessedBy = oo.ProcessedBy;
                    o.Status = oo.Status;
                    o.AddedDate = oo.AddedDate;
                    o.Address = oo.Address;
                    o.Area = oo.Area;
                    o.DeliveredBy = oo.DeliveredBy;

                    o.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "GET", Rel = "All Order" });
                    o.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + o.Id, Method = "GET", Rel = "Specific Order" });
                    o.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + o.Id, Method = "PUT", Rel = "Order Edit" });
                    o.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + o.Id, Method = "DELETE", Rel = "Order Delete" });
                    o.links.Add(new Links() { HRef = "http://localhost:56991/api/orders", Method = "POST", Rel = "Order Create" });

                    oList.Add(o);
                }


                return Ok(oList);
            }

        }
    }
}
