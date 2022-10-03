using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("NewYears")]
    public class NewYear
    {
        [Key]
        public int Id { get; set; }
        public int? Gift { get; internal set; }
        public string Phome { get; internal set; }
    }
}