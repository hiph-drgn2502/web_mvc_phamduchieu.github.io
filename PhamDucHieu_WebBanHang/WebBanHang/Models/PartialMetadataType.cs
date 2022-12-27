using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanHang.Models;

namespace WebBanHang.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        //public object ImageFile { get; internal set; }
        public DateTime CreateOnUtc { get; internal set; }
    }
}