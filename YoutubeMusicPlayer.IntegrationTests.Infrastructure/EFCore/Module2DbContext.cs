using System;
using Microsoft.EntityFrameworkCore;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore
{
    public class Entity2
    {
        public Guid Id { get; private set; }
        public string Value { get; private set; }

        private Entity2() { }
        public Entity2(Guid id, string value)
        {
            Id = id;
            Value = value;
        }
    }

    public class Module2DbContext : DbContext
    {

        public DbSet<Entity2> Entities { get; set; }
        public Module2DbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity2>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Value);
                    e.ToTable("module2_entities");
                }
            );
        }
    }
}