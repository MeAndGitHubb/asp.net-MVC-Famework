using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Tilte { get; set; }
        [Required]
        public string Content { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }
    }
}