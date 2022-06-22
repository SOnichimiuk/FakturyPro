using FakturyPro.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturyPro.Data
{
    public class FakturyDbContext : DbContext
    {
        public FakturyDbContext() : base("name=FakturyDbContext") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDocument> ProductDocuments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDocument>().HasKey(x => new {x.ProductId, x.DocumentId});

            modelBuilder.Entity<Product>().HasMany(x => x.ProductDocuments).WithRequired(x => x.Product).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Document>().HasMany(x => x.ProductDocuments).WithRequired(x => x.Document).HasForeignKey(x => x.DocumentId);
        }
    }
}
