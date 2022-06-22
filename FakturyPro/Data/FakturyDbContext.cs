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
    }
}
