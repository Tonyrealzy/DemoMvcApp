using Microsoft.EntityFrameworkCore;

namespace DemoAppMvc.Models
{
    public class DemoAppDbContext : DbContext //extend the DbContext dependency package we installed from Nuget solution
    {
        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Staff> StaffAll { get; set; } //this is pointing to the staff model. it is depending on the model you are pointing to
    }
}
