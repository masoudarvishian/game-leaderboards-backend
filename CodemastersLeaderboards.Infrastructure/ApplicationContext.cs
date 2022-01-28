using GameLeaderboards.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameLeaderboards.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // config user
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.Username).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PasswordSalt).IsRequired();

            // config country
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().Property(x => x.Name).IsRequired();

            // config platform
            modelBuilder.Entity<Platform>().ToTable("Platforms");
            modelBuilder.Entity<Platform>().HasKey(x => x.Id);
            modelBuilder.Entity<Platform>().Property(x => x.Name).IsRequired();

            // config vehicle
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");
            modelBuilder.Entity<Vehicle>().HasKey(x => x.Id);
            modelBuilder.Entity<Vehicle>().Property(x => x.Name).IsRequired();

            // config Races
            modelBuilder.Entity<Race>().ToTable("Races");
            modelBuilder.Entity<Race>().HasKey(x => x.Id);
            modelBuilder.Entity<Race>().Property(x => x.LapCount).IsRequired();

            // config leaderboard
            modelBuilder.Entity<Leaderboard>().ToTable("Leaderboards");
            modelBuilder.Entity<Leaderboard>().HasKey(x => new { x.UserId, x.RaceId });
            modelBuilder.Entity<Leaderboard>().HasOne(x => x.User).WithMany(x => x.Leaderboards).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Leaderboard>().HasOne(x => x.Race).WithMany(x => x.Leaderboards).HasForeignKey(x => x.RaceId);
            modelBuilder.Entity<Leaderboard>().HasOne(x => x.Country).WithMany(x => x.Leaderboards).HasForeignKey(x => x.CountryId);
            modelBuilder.Entity<Leaderboard>().HasOne(x => x.Vehicle).WithMany(x => x.Leaderboards).HasForeignKey(x => x.VehicleId);
            modelBuilder.Entity<Leaderboard>().HasOne(x => x.Platform).WithMany(x => x.Leaderboards).HasForeignKey(x => x.PlatformId);
            modelBuilder.Entity<Leaderboard>().Property(x => x.Time).IsRequired();
        }

        // This is for db context being in a separate Project
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../CodemastersLeaderboards.API/appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<ApplicationContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);
                return new ApplicationContext(builder.Options);
            }
        }
    }
}
