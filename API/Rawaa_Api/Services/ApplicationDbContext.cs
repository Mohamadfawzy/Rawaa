using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;

namespace Rawaa_Api.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<FileUplod>();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<OldProduct> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users {get; set; }
    }
}
