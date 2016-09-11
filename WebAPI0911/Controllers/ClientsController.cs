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
using WebAPI0911.Models;

namespace WebAPI0911.Controllers
{
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();
        public ClientsController()
        {
            //避免循環參照的問題
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Clients
        [Route("clients")]
        //http://localhost:13838/clients/
        public IQueryable<Client> GetClient()
        {
            return db.Client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("clients/{id}")]
        //http://localhost:13838/clients/1/
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("clients/{id}/orders")]
        //http://localhost:13838/clients/1/orders
        public IHttpActionResult GetClientOrders(int id)
        {
            var orders = db.Order.Where(d => d.ClientId == id);
            return Ok(orders);
        }

        [ResponseType(typeof(Client))]
        [Route("clients/{id}/orders/{orderID}")]
        //http://localhost:13838/clients/1/orders/182
        public IHttpActionResult GetClientOrder(int id, int orderId)
        {

            //var orders = db.Order.Where(d => d.ClientId == id && d.OrderId == orderId);
            var orders = db.Order.FirstOrDefault(d => d.ClientId == id && d.OrderId == orderId);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("clients/{id}/orders/pending")]
        //http://localhost:13838/clients/1/orders/182
        public IHttpActionResult GetClientPending(int id)
        {
            var orders = db.Order.Where(d => d.ClientId == id && d.OrderStatus == "P");
            return Ok(orders);
        }

        [ResponseType(typeof(Client))]
        [Route("clients/{id}/orders/{*date}")]
        //http://localhost:13838/clients/1/orders/2001/05/26
        //注意1=>PostMan傳入的日期格式要等於 2001/05/26  若2001-05-26則判斷不出來
        //注意2=>{*date} == DateTime date 變數名稱需要一致  ModelBinding
        //注意3=>網址使用簡單型別  VS 複雜型別ex:class(Body)
        //注意4=>不會去定義狀態碼回傳甚麼 前後端訂定
        public IHttpActionResult GetClientOrdersByDate(int id,DateTime date)
        {
            var orders = db.Order.Where(d => d.ClientId == id && d.OrderStatus == "P"
                                        && d.OrderDate.Value.Year == date.Year 
                                        && d.OrderDate.Value.Month == date.Month
                                        && d.OrderDate.Value.Day == date.Day
                                        );
            return Ok(orders);
        }





        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}