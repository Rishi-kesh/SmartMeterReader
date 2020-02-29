using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingSmart.Models;

namespace TestingSmart.DataContext
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitHistory> UnitHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TestingSmart.Models.BillAmount> BillAmount { get; set; }

        public DbSet<TestingSmart.Models.BillPayment> BillPayment { get; set; }
    }
}
