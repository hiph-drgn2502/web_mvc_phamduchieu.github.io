using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class ShoppingCartModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}