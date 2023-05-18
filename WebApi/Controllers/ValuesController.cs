using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA;
using WebApi.DTO;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    //[Authorize]
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        COVID19DbContext db = new COVID19DbContext();
        public List<OrderDetailsDTO> Get()
        {
            var order_details = db.VacOrderItem.Select(v => new OrderDetailsDTO()
            {
                countryName = v.VacOrder.Country.Name,
                orderId = v.VacOrder.Id,
                vacName = v.Vaccin.Name,
                amount = v.Amount
            }).ToList();
            return order_details;
        }

        // GET api/values/5
        [Route("api/values/{Name}")]
        public List<OrderDetailsDTO> Get(string Name)
        {
            var order_details = db.VacOrderItem.Where(o => o.VacOrder.Country.Name == Name).Select
                (v => new OrderDetailsDTO()
                {
                    countryName = v.VacOrder.Country.Name,
                    orderId = v.VacOrder.Id,
                    vacName = v.Vaccin.Name,
                    amount = v.Amount
                }).ToList();

            if (order_details != null)
                return order_details;
            return Get();
        }

        //    // POST api/values
        [HttpPost]
        [Route("api/post/update/{orderId}/{vacName}/{amount_}")]
        public void update(int orderId, string vacName, int amount_)
        {
            var k = db.VacOrderItem.Where(x => x.OrderId == orderId).Where(y => y.Vaccin.Name.Equals(vacName)).SingleOrDefault();
            if (k != null)
            {
                k.Vaccin.Quantity = k.Amount - amount_;
                k.Amount = amount_;
                db.SaveChanges();
            }
            // db.VacOrderItem.Where(x=>x.OrderId==orderId).Where(y=>y.Vaccin.Name.Equals(vacName)).SingleOrDefault().Vaccin.Quantity = 
            //db.VacOrderItem.Where(x => x.OrderId == orderId).Where(y => y.Vaccin.Name.Equals(vacName)).SingleOrDefault().Amount-amount_;

            // db.VacOrderItem.Where(x => x.OrderId == orderId).Where(y => y.Vaccin.Name.Equals(vacName)).SingleOrDefault().Amount = amount_;
        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}




//var k=db.VacOrderItem.Where(x=>x.VaccinId==ooo.)
//db.Vaccin.Where(y => y.Name == vacName).SingleOrDefault().Quantity = uod.Amount - amount_;
////uod.Amount = amount_;
//var uod = db.VacOrderItem.Where(x => x.OrderId == orderId).ToList();
//var t=db.Vaccin.Where(y=>y.Id==uod.&& y.Name.Equals(vacName)).SingleOrDefault();
//t.Quantity = uod.Amount - amount_;
//uod.Amount = amount_;




//var uod = db.VacOrderItem.Where(x => x.OrderId == orderId).SingleOrDefault();
//db.Vaccin.Where(y => y.Name == vacName).SingleOrDefault().Quantity = uod.Amount - amount_;
//uod.Amount = amount_;