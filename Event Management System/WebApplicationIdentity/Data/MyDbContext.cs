using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationIdentity.Models;

namespace WebApplicationIdentity.Data
{
    public class MyDbContext:IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<WebApplicationIdentity.Models.Contact>? Contact { get; set; }

        public DbSet<WebApplicationIdentity.Models.EventDetail>? EventDetail { get; set; }

        public DbSet<WebApplicationIdentity.Models.EventCategory>? EventCategory { get; set; }

        public DbSet<WebApplicationIdentity.Models.Payment>? Payment { get; set; }

        public DbSet<WebApplicationIdentity.Models.Schedule>? Schedule { get; set; }

        public DbSet<WebApplicationIdentity.Models.Feedback>? Feedback { get; set; }


    }
}
