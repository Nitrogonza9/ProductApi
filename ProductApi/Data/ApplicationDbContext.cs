namespace ProductApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProductApi.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
