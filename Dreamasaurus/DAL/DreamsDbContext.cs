using Dreamasaurus.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dreamasaurus.DAL
{
    public interface IDreamsDbContext
    {
        DbSet<Dream> Dreams { get; set; }
        DbSet<Tag> Tags { get; set; }
    }

    public class DreamsDbContext : IdentityDbContext<ApplicationUser>, IDreamsDbContext
    {
        public DreamsDbContext() : base("DreamsDbContext")
        {
        }

        public DbSet<Dream> Dreams { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }

}