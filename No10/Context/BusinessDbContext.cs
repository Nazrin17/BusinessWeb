using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using No10.Models;

namespace No10.Context
{
    public class BusinessDbContext:IdentityDbContext<AppUser>
    {
        public BusinessDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Icon> Icons { get; set; }
    }
}
