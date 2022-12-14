using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public string Slug { get; set; }
        public int TableId { get; set; }
        public string Type { get; set; }
    }
}