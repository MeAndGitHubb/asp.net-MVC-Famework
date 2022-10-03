using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Branchs")]
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public int? NameBranch { get; set; }

    }
}