using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ScanAndGo.Models
{
    public class ScanAndGoContext : DbContext
    {
        public ScanAndGoContext()
            : base("name=ScanAndGoContext")
        {

        }
        public virtual DbSet<Product> Products { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AdminLoginModel> AdminLoginModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}