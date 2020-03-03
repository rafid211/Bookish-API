using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_ATP2_API.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {

        ProjectDBEntities context = new ProjectDBEntities();        

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Orders.ToList();
            if (list.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            List<Order> orderlist = new List<Order>();
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


                orderlist.Add(order);
            }
            return Ok(orderlist);

        }

        [Route("{id}", Name = "GetOrderById")]
        public IHttpActionResult Get(int id)
        {
            var item = context.Orders.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

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
        [Route("")]
        public IHttpActionResult Post(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            
            return Created(Url.Link("GetOrderById", new { id = order.Id }), order);

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody]Order o)
        {
            var item = context.Orders.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            item.Id = o.Id;
            item.User_Id = o.User_Id;
            item.Name = o.Name;
            item.PhoneNumber = o.PhoneNumber;
            item.Area = o.Area;
            item.Address = o.Address;
            item.Status = o.Address;
            item.ProcessedBy = o.ProcessedBy;
            item.DeliveredBy = o.DeliveredBy;
            item.AddedDate = o.AddedDate;
            item.ModifiedDate = o.ModifiedDate;
            item.Coupon_Id = o.Coupon_Id;

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

            context.SaveChanges();
            return Ok(order);
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Orders.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            context.Orders.Remove(item);
            context.SaveChanges();          
            return StatusCode(HttpStatusCode.NoContent);
        }

        /* OrderData */
        [Route("{id}/orderdatas")]
        public IHttpActionResult GetOrderData([FromUri]int id)
        {
            var list = context.Orders.Find(id).OrderDatas;

            if (list.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<OrderData> datalist = new List<OrderData>();
            foreach (var item in list)
            {
                OrderData data = new OrderData();
                data.Id = item.Id;
                data.Order_Id = item.Order_Id;
                data.Book_Id = item.Book_Id;
                data.QuantityOrdered = item.QuantityOrdered;
                data.Subtotal = item.Subtotal;
                data.ActualPrice =item.ActualPrice;

                data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas", Method = "GET", Rel = "All OrderData for Order ID: "+id });
                data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/"+data.Id, Method = "GET", Rel = "Specific OrderData For Order ID: "+id });
                data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/"+data.Id, Method = "PUT", Rel = "Order Data Edit For Order ID: " +id});
                data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/" + data.Id, Method = "DELETE", Rel = "Order Data Delete For Order ID: " + id });     
                data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas", Method = "POST", Rel = "OrderData Create For Order ID:"+id });

                datalist.Add(data);
            }
            return Ok(datalist);
        }
        [Route("{id}/orderdatas/{did}",Name ="GetOrderDataById")]
        public IHttpActionResult GetOrderDataById([FromUri]int id,[FromUri]int did)
        {
            var item = context.Orders.Find(id).OrderDatas.ToList().Where(x => x.Id == did).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            OrderData data = new OrderData();
            data.Id = item.Id;
            data.Order_Id = item.Order_Id;
            data.Book_Id = item.Book_Id;
            data.QuantityOrdered = item.QuantityOrdered;
            data.Subtotal = item.Subtotal;
            data.ActualPrice = item.ActualPrice;

            data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas", Method = "GET", Rel = "All OrderData for Order ID: " + id });
            data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/" + data.Id, Method = "GET", Rel = "Specific OrderData For Order ID: " + id });
            data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/" + data.Id, Method = "PUT", Rel = "Order Data Edit For Order ID: " + id });
            data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas/" + data.Id, Method = "DELETE", Rel = "Order Data Delete For Order ID: " + id });
            data.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderdatas", Method = "POST", Rel = "OrderData Create For Order ID:" + id });

            return Ok(data);

        }
        [Route("{id}/orderdatas", Name = "PostOrderData")]
        public IHttpActionResult PostOrderData([FromUri]int id,OrderData data)
        {
            data.Order_Id = id;
            context.OrderDatas.Add(data);
            context.SaveChanges();

            return Created(Url.Link("GetOrderDataById", new { id = data.Order_Id, did = data.Id }), data);

        }
        [Route("{id}/orderdatas/{did}", Name = "PutOrderData")]
        public IHttpActionResult PutOrderData([FromUri]int id,[FromUri]int did,[FromBody]OrderData data)
        {
            var item = context.Orders.Find(id).OrderDatas.Where(x => x.Id == did).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            item.Id = did;
            item.Order_Id = id;
            item.Book_Id = data.Book_Id;
            item.QuantityOrdered = data.QuantityOrdered;
            item.Subtotal = data.Subtotal;
            item.ActualPrice = data.ActualPrice;

            OrderData od = new OrderData();
            od.Id = item.Id;
            od.Order_Id = item.Order_Id;
            od.Book_Id = item.Book_Id;
            od.QuantityOrdered = item.QuantityOrdered;
            od.Subtotal = item.Subtotal;
            od.ActualPrice = item.ActualPrice;

            context.SaveChanges();

            return Ok(od);
        }

        [Route("{id}/orderdatas/{did}", Name = "DeleteOrderData")]
        public IHttpActionResult DeleteOrderData([FromUri]int id,[FromUri]int did)
        {
            var item = context.Orders.Find(id).OrderDatas.Where(x => x.Id == did).FirstOrDefault();

            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            context.OrderDatas.Remove(item);
            context.SaveChanges();
           
            return StatusCode(HttpStatusCode.NoContent);
        }
       
        /* OrderLogs */
        [Route("{id}/orderlogs")]
        public IHttpActionResult GetOrderLogs([FromUri]int id)
        {
            var list = context.Orders.Find(id).OrderLogs.ToList();
            if (list.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<OrderLog> loglist = new List<OrderLog>();
            foreach (var item in list)
            {
                OrderLog log = new OrderLog();
                log.Id = item.Id;
                log.Order_Id = item.Order_Id;
                log.AddedDate = item.AddedDate;
                log.LogDetails = item.LogDetails;

                log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs", Method = "GET", Rel = "All OrderLogs for Order ID: " + id });
                log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs/" + log.Id, Method = "GET", Rel = "Specific OrderLogs For Order ID: " + id });
               // log.links.Add(new Links() { HRef = "http://localhost:17193/api/orders/" + id + "/orderlogs/" + log.Id, Method = "PUT", Rel = "OrderLogs Edit For Order ID: " + id });
                //log.links.Add(new Links() { HRef = "http://localhost:17193/api/orders/" + id + "/orderlogs/" + log.Id, Method = "DELETE", Rel = "OrderLogs Delete For Order ID: " + id });
                log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs", Method = "POST", Rel = "OrderLogs Create For Order ID:" + id });

                loglist.Add(log);
            }
            return Ok(loglist);
        }
        [Route("{id}/orderlogs/{lid}", Name = "GetOrderLogById")]
        public IHttpActionResult GetOrderLogById([FromUri]int id,[FromUri]int lid)
        {
            var item = context.Orders.Find(id).OrderLogs.Where(x => x.Id == lid).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            OrderLog log = new OrderLog();
            log.Id = item.Id;
            log.Order_Id = item.Order_Id;
            log.AddedDate = item.AddedDate;
            log.LogDetails = item.LogDetails;

            log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs", Method = "GET", Rel = "All OrderLogs for Order ID: " + id });
            log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs/" + log.Id, Method = "GET", Rel = "Specific OrderLogs For Order ID: " + id });
            // log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs/" + log.Id, Method = "PUT", Rel = "OrderLogs Edit For Order ID: " + id });
            //log.links.Add(new Links() { HRef = "http://localhost:17193/api/orders/" + id + "/orderlogs/" + log.Id, Method = "DELETE", Rel = "OrderLogs Delete For Order ID: " + id });
            log.links.Add(new Links() { HRef = "http://localhost:56991/api/orders/" + id + "/orderlogs", Method = "POST", Rel = "OrderLogs Create For Order ID:" + id });

            return Ok(log);
        }
        [Route("{id}/orderlogs", Name = "PostOrderLog")]
        public IHttpActionResult PostOrderLog([FromUri]int id, OrderLog log)
        {
            log.Order_Id = id;
            context.OrderLogs.Add(log);
            context.SaveChanges();

            return Created(Url.Link("GetOrderLogById", new { id = log.Order_Id, lid = log.Id }), log);

        }

        

    }
}
