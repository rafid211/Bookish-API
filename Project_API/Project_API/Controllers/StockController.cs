using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project_API.Models;

namespace Project_API.Controllers
{
    [RoutePrefix("api/stocks")]
    public class StockController : ApiController
    {
        private ProjectDBEntities context = new ProjectDBEntities();

        [Route("")]
        public IHttpActionResult Get()
        {
            List<Stock> sList = new List<Stock>();
            var list = context.Stocks.ToList();
            Stock s;
            if (list == null)
                return StatusCode(HttpStatusCode.NoContent);
            foreach(Stock item in list)
            {
                s = new Stock();
                s.Id = item.Id;
                s.Book_Id = item.Book_Id;
                s.OrderStock = item.OrderStock;
                s.PhysicalStock = item.PhysicalStock;
                s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks", Method = "GET", Rel = "Get all" });
                s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/" + s.Id, Method = "GET", Rel = "Get one by stock id" });
                s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/books/" + s.Book_Id, Method = "GET", Rel = "Get one by book id" });
                sList.Add(s);
            }
            return Ok(sList);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var stock = context.Stocks.Where(ss => ss.Id == id).FirstOrDefault();
            if (stock == null)
                return StatusCode(HttpStatusCode.NoContent);
            Stock s = new Stock();
            s.Id = stock.Id;
            s.Book_Id = stock.Book_Id;
            s.OrderStock = stock.OrderStock;
            s.PhysicalStock = stock.PhysicalStock;
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks", Method = "GET", Rel = "Get all" });
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/" + s.Id, Method = "GET", Rel = "Get one by stock id" });
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/books/" + s.Book_Id, Method = "GET", Rel = "Get one by book id" });
            return Ok(s);
        }

        [Route("books/{id}")]
        public IHttpActionResult Getbook(int id)
        {
            var stock = context.Stocks.Where(ss => ss.Book_Id == id).FirstOrDefault();
            if (stock == null)
                return StatusCode(HttpStatusCode.NoContent);
            Stock s = new Stock();
            s.Id = stock.Id;
            s.Book_Id = stock.Book_Id;
            s.OrderStock = stock.OrderStock;
            s.PhysicalStock = stock.PhysicalStock;
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks", Method = "GET", Rel = "Get all" });
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/" + s.Id, Method = "GET", Rel = "Get one by stock id" });
            s.links.Add(new Links() { HRef = "http://localhost:56991/api/stocks/books/" + s.Book_Id, Method = "GET", Rel = "Get one by book id" });
            return Ok(s);
        }
    }
}
