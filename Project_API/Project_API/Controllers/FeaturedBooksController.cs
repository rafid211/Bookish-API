using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_API.Controllers
{
    [RoutePrefix("api/featuredbooks")]
    public class FeaturedBooksController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Book> bList = new List<Book>();
            var list = context.FeaturedBooks.ToList();
            Book b;
            if (list == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {
                foreach (FeaturedBook item in list)
                {
                    b = new Book();
                    b.Id = item.Book_Id;
                    b.Author_Id = item.Book.Author_Id;
                    b.Category_Id = item.Book.Category_Id;
                    b.Country = item.Book.Country;
                    b.DiscountRate = item.Book.DiscountRate;
                    b.Edition = item.Book.Edition;
                    b.Image = item.Book.Image;
                    b.ISBN = item.Book.ISBN;
                    b.Language = item.Book.Language;
                    b.NumberOfPage = item.Book.NumberOfPage;
                    b.Price = item.Book.Price;
                    b.Publisher_Id = item.Book.Publisher_Id;
                    b.Summary = item.Book.Summary;
                    b.Title = item.Book.Title;
                    b.Author = null;
                    b.Category = null;
                    b.FeaturedBooks = null;
                    b.OrderDatas = null;
                    b.Publisher = null;
                    b.Ratings = null;
                    b.Reports = null;
                    b.Stocks = null;

                    b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "GET", Rel = "Self" });
                    b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "GET", Rel = "Specific Resource" });
                    b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "PUT", Rel = "Resource Edit" });
                    b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "DELETE", Rel = "Resource Delete" });
                    b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "POST", Rel = "Resource Create" });

                    bList.Add(b);
                }
                return Ok(bList);
            }

        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Book b = new Book();
            var item = context.FeaturedBooks.Where(db => db.Book_Id == id).FirstOrDefault().Book;

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                b.Id = item.Id;
                b.Author_Id = item.Author_Id;
                b.Category_Id = item.Category_Id;
                b.Country = item.Country;
                b.DiscountRate = item.DiscountRate;
                b.Edition = item.Edition;
                b.Image = item.Image;
                b.ISBN = item.ISBN;
                b.Language = item.Language;
                b.NumberOfPage = item.NumberOfPage;
                b.Price = item.Price;
                b.Publisher_Id = item.Publisher_Id;
                b.Summary = item.Summary;
                b.Title = item.Title;
                b.Author = null;
                b.Category = null;
                b.FeaturedBooks = null;
                b.OrderDatas = null;
                b.Publisher = null;
                b.Ratings = null;
                b.Reports = null;
                b.Stocks = null;

                b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "GET", Rel = "Self" });
                b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "GET", Rel = "Specific Resource" });
                b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "PUT", Rel = "Resource Edit" });
                b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + b.Id, Method = "DELETE", Rel = "Resource Delete" });
                b.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "POST", Rel = "Resource Create" });


                return Ok(b);
            }

        }

        [Route("{id}")]
        public IHttpActionResult PUT([FromUri]int id, [FromBody]FeaturedBook fb)
        {

            var item = context.FeaturedBooks.Where(c => c.Id == id).FirstOrDefault();

            if (item == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {
                item.Book_Id = fb.Book_Id;
                context.SaveChanges();
                return Ok(item);
            }

        }

        [Route("")]
        public IHttpActionResult Post(FeaturedBook fb)
        {

            if (fb == null)
                return StatusCode(HttpStatusCode.NoContent);
            else
            {

                context.FeaturedBooks.Add(fb);
                context.SaveChanges();


                return Ok(fb);
            }

        }

       
    }
}
