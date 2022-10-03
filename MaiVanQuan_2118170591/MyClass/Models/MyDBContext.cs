using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyClass.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("name=StrConnect") { }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Config> Configs { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Hot> Hots { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<NewYear> NewYears { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<MyClass.Models.Link> Links { get; set; }
    }
}
