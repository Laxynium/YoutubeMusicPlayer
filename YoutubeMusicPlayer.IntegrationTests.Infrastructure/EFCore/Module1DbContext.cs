using System;
using Microsoft.EntityFrameworkCore;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore
{
    public class Entity1
    {
        public Guid Id { get; private set; }
        public string Value { get; private set; }

        private Entity1() { }
        public Entity1(Guid id, string value)
        {
            Id = id;
            Value = value;
        }
    }
    public class Module1DbContext : DbContext
    {
        public DbSet<Entity1> Entities { get; set; }
        public Module1DbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity1>(
                e =>
                {
                    e.HasKey(x => x.Id);
                    e.Property(x => x.Value);
                    e.ToTable("module1_entities");
                }
            );
        }
    }
}