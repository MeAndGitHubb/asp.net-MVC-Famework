using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }
        public int? Sale { get; set; }
        public string Note { get; set; }
        public int Status { get; internal set; }
    }
}