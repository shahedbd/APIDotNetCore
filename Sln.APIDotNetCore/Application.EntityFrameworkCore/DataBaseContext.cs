using Application.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.EntityFrameworkCore
{
    public class DataBaseContext : IdentityDbContext<AppUser>
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}
