using Invitation_App_Web_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invitation_App_Web_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}
