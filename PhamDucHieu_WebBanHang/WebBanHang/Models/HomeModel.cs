using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<ProductBrand> ListProductBrand { get; set; }
        public List<SaleProduct> ListSaleProduct { get; set; }
        public List<ProductAccessory> ListProductAccessory { get; set; }
        public List<ProductRecommended> ListProductRecommended { get; set; }
        public List<Category> ListCategory { get; set; }
    }
}