using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project_API.Models;

namespace Project_ATP2_API.Controllers
{
    [RoutePrefix("api/deliverymans")]
    public class DeliveryManController : ApiController
    {
        ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.DeliveryMans.ToList();
            if (list.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<DeliveryMan> manlist = new List<DeliveryMan>();
            foreach (var item in list)
            {
                DeliveryMan man = new DeliveryMan();
                man.Id = item.Id;
                man.Area = item.Area;
                man.User_Id = item.User_Id;

                man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans", Method = "GET", Rel = "All Deliverymans" });
                man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "GET", Rel = "Specific deliveryman" });
                man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "PUT", Rel = "Deliveryman Edit" });
                man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "DELETE", Rel = "Deliveryman Delete" });
                man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans", Method = "POST", Rel = "Deliveryman Create" });

                manlist.Add(man);
            }
            return Ok(manlist);
        }
        [Route("{id}",Name ="GetDeliverymanById")]
        public IHttpActionResult GetDeliverymanById([FromUri]int id)
        {
            var item = context.DeliveryMans.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            DeliveryMan man = new DeliveryMan();
            man.Id = item.Id;
            man.Area = item.Area;
            man.User_Id = item.User_Id;

            man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans", Method = "GET", Rel = "All Deliverymans" });
            man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "GET", Rel = "Specific deliveryman" });
            man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "PUT", Rel = "Deliveryman Edit" });
            man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + man.Id, Method = "DELETE", Rel = "Deliveryman Delete" });
            man.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans", Method = "POST", Rel = "Deliveryman Create" });

            return Ok(man);

        }

        [Route("")]
        public IHttpActionResult Post(DeliveryMan del)
        {
            context.DeliveryMans.Add(del);
            context.SaveChanges();

            return Created(Url.Link("GetDeliverymanById", new { id = del.Id }), del);

        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody]DeliveryMan del)
        {
            var item = context.DeliveryMans.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            item.Id = del.Id;
            item.User_Id = del.User_Id;
            item.Area = del.Area;


            DeliveryMan man = new DeliveryMan();
            man.Id = item.Id;
            man.User_Id = item.User_Id;
            man.Area = item.Area;

            context.SaveChanges();
            return Ok(man);
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.DeliveryMans.Find(id);
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            context.DeliveryMans.Remove(item);
            context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /* Delivery Task*/

        [Route("{id}/deliverytasks")]
        public IHttpActionResult GetDeliverytasks([FromUri]int id)
        {
            var list = context.DeliveryMans.Find(id).DeliveryTasks;

            if (list.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<DeliveryTask> tasklist = new List<DeliveryTask>();
            foreach (var item in list)
            {
                DeliveryTask task = new DeliveryTask();
                task.Id = item.Id;
                task.DeliveryMan_Id = item.DeliveryMan_Id;
                task.Order_Id = item.Order_Id;
                task.Status = item.Status;
                task.AddedDate = item.AddedDate;
                task.TimeTaken = item.TimeTaken;

                task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks", Method = "GET", Rel = "All DeliveryTask for deliveryMan ID: " + id });
                task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "GET", Rel = "Specific DeliveryTask For deliveryMan ID: " + id });
                task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "PUT", Rel = "DeliveryTask Edit For deliveryMan ID: " + id });
                task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "DELETE", Rel = "DeliveryTask Delete For deliveryMan ID: " + id });
                task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks", Method = "POST", Rel = "DeliveryTask Create For deliveryMan ID:" + id });

                tasklist.Add(task);
            }
            return Ok(tasklist);
        }

        [Route("{id}/deliverytasks/{tid}", Name = "GetDeliverTaskById")]
        public IHttpActionResult GetDeliverTaskById([FromUri]int id, [FromUri]int tid)
        {
            var item = context.DeliveryMans.Find(id).DeliveryTasks.ToList().Where(x => x.Id == tid).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            DeliveryTask task = new DeliveryTask();
            task.Id = item.Id;
            task.DeliveryMan_Id = item.DeliveryMan_Id;
            task.Order_Id = item.Order_Id;
            task.Status = item.Status;
            task.AddedDate = item.AddedDate;
            task.TimeTaken = item.TimeTaken;

            task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks", Method = "GET", Rel = "All DeliveryTask for deliveryMan ID: " + id });
            task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "GET", Rel = "Specific DeliveryTask For deliveryMan ID: " + id });
            task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "PUT", Rel = "DeliveryTask Edit For deliveryMan ID: " + id });
            task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks/" + task.Id, Method = "DELETE", Rel = "DeliveryTask Delete For deliveryMan ID: " + id });
            task.links.Add(new Links() { HRef = "http://localhost:56991/api/deliverymans/" + id + "/deliverytasks", Method = "POST", Rel = "DeliveryTask Create For deliveryMan ID:" + id });

            return Ok(task);

        }

        [Route("{id}/deliverytasks", Name = "PostDeliveryTask")]
        public IHttpActionResult PostDeliveryTask([FromUri]int id, DeliveryTask task)
        {
            task.DeliveryMan_Id = id;
            context.DeliveryTasks.Add(task);
            context.SaveChanges();

            return Created(Url.Link("GetDeliverTaskById", new { id = task.DeliveryMan_Id, tid = task.Id }), task);

        }

        [Route("{id}/deliverytasks/{tid}", Name = "PutDeliveryTask")]
        public IHttpActionResult PutDeliveryTask([FromUri]int id, [FromUri]int tid, [FromBody]DeliveryTask task)
        {
            var item = context.DeliveryMans.Find(id).DeliveryTasks.Where(x => x.Id == tid).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                //item.Id = tid;
                //item.Order_Id = task.Order_Id;
                //item.DeliveryMan_Id = task.DeliveryMan_Id;
                item.Status = task.Status;
                item.AddedDate = task.AddedDate;
                item.TimeTaken = task.TimeTaken;


                DeliveryTask t = new DeliveryTask();
                t.Id = item.Id;
                t.Order_Id = item.Order_Id;
                t.DeliveryMan_Id = item.DeliveryMan_Id;
                t.Status = item.Status;
                task.AddedDate = item.AddedDate;
                task.TimeTaken = item.TimeTaken;


                context.SaveChanges();

                return Ok(t);
            }
        }

        [Route("{id}/deliverytasks/{tid}", Name = "DeleteDeliveryTask")]
        public IHttpActionResult DeleteDeliveryTask([FromUri]int id, [FromUri]int tid)
        {
            var item = context.DeliveryMans.Find(id).DeliveryTasks.Where(x => x.Id == tid).FirstOrDefault();

            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            context.DeliveryTasks.Remove(item);
            context.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}

