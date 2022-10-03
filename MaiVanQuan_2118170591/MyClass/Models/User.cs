using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ConfimPassword { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Roles { get; set; }
        public string Img { get; set; }
        public int? CountError { get; set; }
        
        
        public string Gender { get; set; }
        public string Address { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}