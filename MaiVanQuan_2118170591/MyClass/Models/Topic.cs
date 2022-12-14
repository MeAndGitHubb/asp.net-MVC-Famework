using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        [Required]
        public string Metakey { get; set; }
        [Required]
        public string Metadesc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}