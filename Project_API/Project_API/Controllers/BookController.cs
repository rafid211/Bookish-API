using Project_API.Attributes;
using Project_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_API.Controllers
{
    public class BookController : ApiController
    {
        [RoutePrefix("api/Books")]
        public class PostController : ApiController
        {
            private ProjectDBEntities context = new ProjectDBEntities();

            [BasicAuthentication]
            [Route("")]
            public IHttpActionResult Get()
            {
                //var list = context.Books.Select(m => new
                //{
                //    m.Author_Id,
                //    m.Category_Id,
                //    m.Country,
                //    m.DiscountRate,
                //    m.Edition,
                //    m.Id,
                //    m.Image,
                //    m.ISBN,
                //    m.Language,
                //    m.NumberOfPage,
                //    m.Price,
                //    m.Publisher_Id,
                //    m.Summary,
                //    m.Title
                //});


                var list = context.Books.ToList();
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

                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "GET", Rel = "Self" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "POST", Rel = "Resource Create" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Author", Method = "GET", Rel = "Get Author" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Category", Method = "GET", Rel = "Get Category" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Publisher", Method = "GET", Rel = "Get Publisher" });

                    books.Add(obj);
                }

                //foreach (var p in list)
                //{
                //    p.Author.Books = null;
                //    foreach (var buy in p.Buys)
                //    {
                //        buy.Book = null;
                //    }
                //    foreach (var item in p.Carts)
                //    {
                //        item.Book = null;
                //    }
                //    p.Category.Books = null;
                //    p.
                //}
                return Ok(books);
            }

            [Route("{id}", Name = "GetBookById")]
            public IHttpActionResult Get(int id)
            {
                //var book = context.Books.Find(id);
                var item = context.Books.Find(id);
                if (item == null)
                {
                    return NotFound();
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

                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "GET", Rel = "All Resourse" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "GET", Rel = "Self" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "POST", Rel = "Resource Create" });
                //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Author", Method = "GET", Rel = "Get Author" });
                //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Category", Method = "GET", Rel = "Get Category" });
                //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Publisher", Method = "GET", Rel = "Get Publisher" });


                return Ok(obj);
            }

            [Route("{id}")]
            public IHttpActionResult Put([FromUri]int id, [FromBody]Book item)
            {
                var obj = context.Books.Where(m => m.Id == id).FirstOrDefault();
                if (item == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
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


                    context.SaveChanges();
                    return Ok();
                }
            }

            [Route("")]
            public IHttpActionResult Post(Book book)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Books.Add(book);
                context.SaveChanges();

                return Created(Url.Link("GetBookById", new { id = book.Id }), book);
            }

            [Route("{id}")]
            public IHttpActionResult Delete([FromUri]int id)
            {
                var item = context.Books.Where(p => p.Id == id).FirstOrDefault();
                if (item == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    context.Books.Remove(item);
                    context.SaveChanges();
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }

            //[Route("{id}/Author", Name = "GetAuthorByBookId")]
            //public IHttpActionResult GetAuthorByBookId([FromUri]int id)
            //{
            //    var item = context.Authors.Where(m => m.Id == id).FirstOrDefault();

            //    if (item == null)
            //    {
            //        return StatusCode(HttpStatusCode.NoContent);
            //    }
            //    Author obj = new Author();
            //    obj.Description = item.Description;
            //    obj.Id = item.Id;
            //    obj.Image = item.Image;
            //    obj.Name = item.Name;
            //    obj.Books = null;

            //    return Ok(obj);
            //}

            //[Route("{id}/Category", Name = "GetCategoryByBookId")]
            //public IHttpActionResult GetCategoryByBookId([FromUri]int id)
            //{
            //    var item = context.Categories.Where(m => m.Id == id).FirstOrDefault();

            //    if (item == null)
            //    {
            //        return StatusCode(HttpStatusCode.NoContent);
            //    }
            //    Category obj = new Category();
            //    obj.Shelf = item.Shelf;
            //    obj.Id = item.Id;
            //    obj.Name = item.Name;
            //    obj.Books = null;

            //    return Ok(obj);
            //}

            //[Route("{id}/Publisher", Name = "GetPublisherByBookId")]
            //public IHttpActionResult GetPublisherByBookId([FromUri]int id)
            //{
            //    var item = context.Publishers.Where(m => m.b == id).FirstOrDefault();

            //    if (item == null)
            //    {
            //        return StatusCode(HttpStatusCode.NoContent);
            //    }
            //    Publisher obj = new Publisher();
            //    obj.Image = item.Image;
            //    obj.Id = item.Id;
            //    obj.Name = item.Name;
            //    obj.Books = null;

            //    return Ok(obj);
            //}


            [Route("{id}/OrderDatas", Name = "GetOrderDatasByBookId")]
            public IHttpActionResult GetOrderDatasByBookId([FromUri]int id)
            {
                var list = context.OrderDatas.Where(m => m.Book_Id == id).Select(m => new
                {
                    m.ActualPrice,
                    m.Book_Id,
                    m.Id,
                    m.Order_Id,
                    m.QuantityOrdered,
                    m.Subtotal
                });
                if (list == null)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }


                return Ok(list);
            }




            //[Route("{Book}/User/{userId}", Name ="AddCartForUser")]
            //public IHttpActionResult PostAddCartForUser()
            //{
            //    var list = context.Carts.ToList();
            //    List<Cart> carts = new List<Cart>();
            //    foreach (var item in list)
            //    {
            //        Cart obj = new Cart();
            //        obj.Book = null;
            //        obj.Book_Id = item.Book_Id;
            //        obj.Id = item.Id;
            //        obj.QuantityOrdered = item.QuantityOrdered;
            //        obj.User = null;
            //        obj.User_Id = item.User_Id;

            //        obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "GET", Rel = "Self" });
            //        obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
            //        obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
            //        obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
            //        obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Carts", Method = "POST", Rel = "Resource Create" });

            //        carts.Add(obj);
            //    }
            //    return Ok(carts);
            //}


            [Route("Search", Name = "GetBookBySearch")]
            public IHttpActionResult PostSearchBook(SearchModel model)
            {

                var list = context.Books.ToList();
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

                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "GET", Rel = "Self" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "GET", Rel = "Specific Resource" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "PUT", Rel = "Resource Edit" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/" + obj.Id, Method = "DELETE", Rel = "Resource Delete" });
                    obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books", Method = "POST", Rel = "Resource Create" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Author", Method = "GET", Rel = "Get Author" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Category", Method = "GET", Rel = "Get Category" });
                    //obj.links.Add(new Links() { HRef = "http://localhost:56991/api/Books/{id}/Publisher", Method = "GET", Rel = "Get Publisher" });

                    books.Add(obj);
                }

                //foreach (var p in list)
                //{
                //    p.Author.Books = null;
                //    foreach (var buy in p.Buys)
                //    {
                //        buy.Book = null;
                //    }
                //    foreach (var item in p.Carts)
                //    {
                //        item.Book = null;
                //    }
                //    p.Category.Books = null;
                //    p.
                //}
                return Ok(books);
            }


        }
    }
}
