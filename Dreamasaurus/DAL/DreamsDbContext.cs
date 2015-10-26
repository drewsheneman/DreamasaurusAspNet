using Dreamasaurus.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dreamasaurus.DAL
{
    public class DreamsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DreamsDbContext() : base("DreamsDbContext")
        {
        }

        public DbSet<Dream> Dreams { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}