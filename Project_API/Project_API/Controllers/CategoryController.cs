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
    [RoutePrefix("api/Categories")]
    public class CategoryController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();
        private ProjectDBEntities context = new ProjectDBEntities();

        public CategoryController()
        {
            // Add the following code
            // problem will be solved
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var list = context.Categories.ToList();
            List<Category> categories = new List<Category>();
            foreach (var item in list)
            {
                Category obj = new Category();
                obj.Books = null;
                obj.Shelf = item.Shelf;
                obj.Id = item.Id;
                obj.Name = item.Name;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories", Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories", Method = "POST", Rel = "Resource Create" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Category" });


                categories.Add(obj);
            }
            return Ok(categories);
        }

        [Route("{id}", Name = "GetCategoryById")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory([FromUri]int id)
        {
            Category obj = db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories", Method = "GET", Rel = "All Resource" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "GET", Rel = "Self" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories", Method = "POST", Rel = "Resource Create" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Id + "/Books", Method = "GET", Rel = "All Books By Category" });

            return Ok(obj);
        }


        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory([FromUri]int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        [Route("")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("{id}/Books", Name = "GetBooksByCategory")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetBooksByCategory([FromUri]int id)
        {
            var list = context.Books.Where(m => m.Category_Id == id).ToList();

            if (list == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            List<Book> books = new List<Book>();
            foreach (var item in list)
            {
                Book obj = new Book();
                obj.Author_Id = item.Author_Id;
                obj.Category_Id = item.Category_Id;
                obj.Country = item.Country;
                obj.DiscountRate = item.DiscountRate;
                obj.Edition = item.Edition;
                obj.Id = item.Id;
                obj.Image = item.Image;
                obj.ISBN = item.ISBN;
                obj.Language = item.Language;
                obj.NumberOfPage = item.NumberOfPage;
                obj.Price = item.Price;
                obj.Publisher_Id = item.Publisher_Id;
                obj.Summary = item.Summary;
                obj.Title = item.Title;
                obj.Author = null;
                obj.Category = null;
                obj.FeaturedBooks = null;
                obj.OrderDatas = null;
                obj.Publisher = null;
                obj.Ratings = null;
                obj.Reports = null;
                obj.Stocks = null;

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Category_Id + "/Books", Method = "GET", Rel = "All Books BY Category" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Category_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Category" });

                books.Add(obj);
            }
            return Ok(books);
        }

        [Route("{id}/Books/{bookId}", Name = "GetBookByCategory")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetBookByCategory([FromUri]int id, [FromUri]int bookId)
        {
            var item = context.Books.Where(m => m.Category_Id == id && m.Id == bookId).FirstOrDefault();

            if (item == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            Book obj = new Book();
            obj.Author_Id = item.Author_Id;
            obj.Category_Id = item.Category_Id;
            obj.Country = item.Country;
            obj.DiscountRate = item.DiscountRate;
            obj.Edition = item.Edition;
            obj.Id = item.Id;
            obj.Image = item.Image;
            obj.ISBN = item.ISBN;
            obj.Language = item.Language;
            obj.NumberOfPage = item.NumberOfPage;
            obj.Price = item.Price;
            obj.Publisher_Id = item.Publisher_Id;
            obj.Summary = item.Summary;
            obj.Title = item.Title;
            obj.Author = null;
            obj.Category = null;
            obj.FeaturedBooks = null;
            obj.OrderDatas = null;
            obj.Publisher = null;
            obj.Ratings = null;
            obj.Reports = null;
            obj.Stocks = null;


            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Category_Id + "/Books", Method = "GET", Rel = "All Books BY Category" });
            obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Categories/" + obj.Category_Id + "/Books/" + obj.Id, Method = "GET", Rel = "Book By Category" });


            return Ok(obj);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}