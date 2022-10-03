using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Recruitments")]
    public class Recruitment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}