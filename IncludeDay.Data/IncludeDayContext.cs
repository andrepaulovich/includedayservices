using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using IncludeDay.Data.Entities;

namespace IncludeDay.Data
{
    public class IncludeDayContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Permissões
            modelBuilder.Entity<Location>()
                .HasMany(c => c.Feedbacks)
                .WithRequired();

        }
    }
}
