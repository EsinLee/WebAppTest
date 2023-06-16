using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebAppTest.Models.Domain;

namespace WebAppTest.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Member> Members {get;set;}
        public DbSet<Employee> Employees { get;set;}
        public DbSet<Security> Securitys { get;set;}
        public DbSet<CashFlow> CashFlows { get;set;}
        public DbSet<Receipt> Receipts { get;set;}
        public DbSet<AccountName> AccountNames { get;set;}
    }
}
