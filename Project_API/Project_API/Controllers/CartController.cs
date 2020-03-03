using Project_API.Models;
using Project_API.Models.CustomRakib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_API.Controllers
{
    [RoutePrefix("api/Carts")]
    public class CartController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Carts.ToList();
            List<Cart> carts = new List<Cart>();
            foreach (var item in list)
            {
                CartRakib obj = new CartRakib();
                
                obj.Book_Id = item.Book_Id;
                obj.Id = item.Id;
                obj.User = null;
                obj.QuantityOrdered = item.QuantityOrdered;
                obj.Book = null;
                obj.Price = item.Book.Price;
                obj.User_Id = item.User_Id;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.User_Id, Method = "GET", Rel = "Specific Resource For User" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "POST", Rel = "Resource Create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.User_Id, Method = "GET", Rel = "Get Carts For User" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.User_Id, Method = "PUT", Rel = "Edit Quantity For User" });


                carts.Add(obj);
            }

            return Ok(carts);
        }

        [Route("Users/{userId}", Name = "GetCartsByUserId")]
        public IHttpActionResult GetCartsByUserId([FromUri]int userId)
        {
            //var book = context.books.find(id);
            var list = context.Carts.Where(m => m.User_Id == userId).ToList();
            if (list == null)
            {
                return NotFound();
            }
            List<Cart> carts = new List<Cart>();
            foreach (var item in list)
            {
                Cart obj = new Cart();
                obj.Book = null;
                obj.Book_Id = item.Book_Id;
                obj.Id = item.Id;
                obj.QuantityOrdered = item.QuantityOrdered;
                obj.User = null;
                obj.User_Id = item.User_Id;
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "get", Rel = "all resourse" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "put", Rel = "resource edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "delete", Rel = "resource delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "post", Rel = "resource create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "get", Rel = "self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.User_Id, Method = "GET", Rel = "Get Carts For User" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.User_Id, Method = "PUT", Rel = "Edit Quantity For User" });
                carts.Add(obj);
            }

            return Ok(carts);
        }

        [Route("{id}", Name = "GetCartById")]
        public IHttpActionResult Get(int id)
        {
            //var book = context.Books.Find(id);
            var item = context.Carts.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            Cart obj = new Cart();
            obj.Book = null;
            obj.Book_Id = item.Book_Id;
            obj.Id = item.Id;
            obj.QuantityOrdered = item.QuantityOrdered;
            obj.User = null;
            obj.User_Id = item.User_Id;

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "GET", Rel = "All Resourse" });
            //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "POST", Rel = "Resource Create" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/carts/Users" + obj.User_Id, Method = "GET", Rel = "Get Carts For User" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/Users" + obj.User_Id, Method = "PUT", Rel = "Edit Quantity For User" });

            return Ok(obj);
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri]int id, [FromBody]Cart item)
        {
            var obj = context.Carts.Where(m => m.Id == id).FirstOrDefault();
            if (obj == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                obj.User = null;
                obj.QuantityOrdered = item.QuantityOrdered;
                obj.User = null;

                context.SaveChanges();
                return Ok();
            }

        }

        [Route("")]
        public IHttpActionResult Post(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Carts.Add(cart);
            context.SaveChanges();

            return Created(Url.Link("GetCartById", new { id = cart.Id }), cart);
        }

        [Route("{id}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            var item = context.Carts.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                context.Carts.Remove(item);
                context.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("Users/{userId}", Name = "PutCartsByUserId")]
        public IHttpActionResult PutCartsByUserId([FromUri]int userId, [FromBody]Cart item)
        {
            var obj = context.Carts.Where(m => m.User_Id == userId).FirstOrDefault();
            if (obj == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                obj.User = null;
                obj.QuantityOrdered = item.QuantityOrdered;
                obj.User = null;

                context.SaveChanges();
                return Ok();
            }
        }

        [Route("Users/{userId}/Books", Name = "GetCartBooksByUserId")]
        public IHttpActionResult GetCartBooksByUserId([FromUri]int userId, [FromBody]Cart item)
        {
            var list = context.Carts.Where(m => m.User_Id == userId).ToList();

            if (list == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<Book> books = new List<Book>();
            foreach (var ite in list)
            {
                Book obj = new Book();
                obj.Author_Id = ite.Book.Author_Id;
                obj.Category_Id = ite.Book.Category_Id;
                obj.Country = ite.Book.Country;
                obj.DiscountRate = ite.Book.DiscountRate;
                obj.Edition = ite.Book.Edition;
                obj.Id = ite.Book.Id;
                obj.Image = ite.Book.Image;
                obj.ISBN = ite.Book.ISBN;
                obj.Language = ite.Book.Language;
                obj.NumberOfPage = ite.Book.NumberOfPage;
                obj.Price = ite.Book.Price;
                obj.Publisher_Id = ite.Book.Publisher_Id;
                obj.Summary = ite.Book.Summary;
                obj.Title = ite.Book.Title;
                obj.Author = null;
                obj.Category = null;
                obj.FeaturedBooks = null;
                obj.OrderDatas = null;
                obj.Publisher = null;
                obj.Ratings = null;
                obj.Reports = null;
                obj.Stocks = null;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books", Method = "GET", Rel = "All Books BY Author" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Authors/" + obj.Author_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Author" });

                books.Add(obj);
            }
            return Ok(books);
        }



    }
}
