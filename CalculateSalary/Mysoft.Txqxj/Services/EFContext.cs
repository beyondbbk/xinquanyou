using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mysoft.Tjqxj.Models.Db;

namespace Mysoft.Tjqxj.Services
{
    public class EFContext:DbContext
    {
        public DbSet<FeedBack> FeedBacks
        {
            get;
            set;
        }
        public DbSet<Useradvise> UserAdvises
        {
            get;
            set;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("Server=localhost;port=3306;database=tjqxj;uid=root;pwd=3565759@bbk");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FeedBack>().ToTable("feedback");
            modelBuilder.Entity<Useradvise>().ToTable("useradvise");
            modelBuilder.Entity<Product>().ToTable("product");
        }
    }
}
