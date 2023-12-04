using IncomeTax.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace IncomeTax.DbContexts
{
    public class IncomeTaxDbContext : DbContext
    {
        public DbSet<IncomeTaxRequest> IncomeTaxRequest { get; set; }

        public IncomeTaxDbContext(DbContextOptions<IncomeTaxDbContext> options) : base(options)
        {
        }
    }
}
