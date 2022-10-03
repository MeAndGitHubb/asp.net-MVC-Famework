using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public string CatName { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public string Detail { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public int Number { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
    }
}
