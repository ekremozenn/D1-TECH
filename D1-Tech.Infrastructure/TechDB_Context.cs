using D1_Tech.Core.Models.PageEntity;
using Microsoft.EntityFrameworkCore;

namespace D1_Tech.Infrastructure
{
    public class TechDB_Context : DbContext
    {
        public TechDB_Context(DbContextOptions<TechDB_Context> options) : base(options) { }

        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceDetail> PlaceDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Model oluşturma kodlarınızı buraya ekleyin
        }
    }
}
