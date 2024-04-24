using Microsoft.EntityFrameworkCore;
using MMORPG.Domain;
using MMORPG.Domain.Entity;
using System.Reflection.Metadata;

namespace MMORPG.Infrastrutture
{
    public class MMORPGDbContext : DbContext
    {

        public MMORPGDbContext() { }

        public MMORPGDbContext(DbContextOptions<MMORPGDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<CharacterEntity> Characters { get; set; }

        public DbSet<UserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.Characters)
                .WithOne(e => e.User)
                .HasForeignKey("UserId")
                .IsRequired();

            
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity() { Id = 1, Username = "Andre" },
                new UserEntity() { Id = 2, Username = "Mazza" },
                new UserEntity() { Id = 3, Username = "Aba" },
                new UserEntity() { Id = 4, Username = "Axel" }
            );
        }

    }
}
