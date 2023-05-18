using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.DTO
{
    public class UpdateOrderDTO
    {
        public int orderId;
        public string vacName;
        public int amount;
        public int quantity;
    }
}