using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Fix { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int Status { get; internal set; }
    }
}