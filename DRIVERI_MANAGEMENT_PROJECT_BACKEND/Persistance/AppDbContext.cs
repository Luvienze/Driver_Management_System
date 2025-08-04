using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Task = Entities.Models.Task;

namespace Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Chieftiency> Chieftiencies { get; set; }
        public DbSet<Chief> Chiefs { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Route> Routes{ get; set; }
        public DbSet<Task> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
              
                entity.Property(e => e.RoleName).IsRequired();
               
                entity.HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.BloodGroup).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.RegistrationNumber).IsUnicode(true).IsRequired().HasMaxLength(6);
                entity.Property(e => e.DateOfStart).IsRequired();
                entity.Property(e => e.IsDeleted).IsRequired();
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.GarageName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.GarageAddress).IsRequired().HasMaxLength(200);

                entity.HasMany(g => g.Drivers)
                      .WithOne(d => d.Garage)
                      .HasForeignKey(d => d.GarageId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.Chiefs)
                      .WithOne(c => c.Garage)
                      .HasForeignKey(c => c.GarageId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(g => g.Vehicles)
                      .WithOne(v => v.Garage)
                      .HasForeignKey(v => v.GarageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Chieftiency>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ChieftiencyName).IsRequired().HasMaxLength(50);

                entity.HasOne(e => e.Garage)
                    .WithMany()
                    .HasForeignKey(e => e.GarageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity<Chief>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.IsActive).IsRequired();

                entity.HasOne(e => e.Person)
                     .WithOne()
                     .HasForeignKey<Chief>(e => e.PersonId)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();

                entity.HasOne(e => e.Garage)
                    .WithMany(g => g.Chiefs)
                    .HasForeignKey(e => e.GarageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasMany(c => c.Drivers)
                    .WithOne(d => d.Chief)
                    .HasForeignKey(d => d.ChiefId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Cadre).IsRequired();
                entity.Property(e => e.DayOff).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();

                entity.HasOne(e => e.Person)
                    .WithOne(p => p.Driver)
                    .HasForeignKey<Driver>(e => e.PersonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.Garage)
                    .WithMany(g => g.Drivers)
                    .HasForeignKey(e => e.GarageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.Chief)
                    .WithMany(c => c.Drivers)
                    .HasForeignKey(e => e.ChiefId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.LineCode).IsRequired().HasMaxLength(6);
                entity.Property(e => e.LineName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IsActive).IsRequired();
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RouteName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Distance).IsRequired();
                entity.Property(e => e.Duration).IsRequired();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DoorNo).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.FuelTank).IsRequired();
                entity.Property(e => e.Plate).IsRequired().HasMaxLength(15);
                entity.Property(e => e.PersonOnFoot).IsRequired();
                entity.Property(e => e.PersonOnSit).IsRequired();
                entity.Property(e => e.SuitableForDisabled).IsRequired();
                entity.Property(e => e.ModelYear).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(e => e.Garage)
                    .WithMany(g => g.Vehicles)
                    .HasForeignKey(e => e.GarageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Direction).IsRequired();
                entity.Property(e => e.DateOfStart).IsRequired();
                entity.Property(e => e.DateOfEnd).IsRequired();
                entity.Property(e => e.PassengerCount).IsRequired();
                entity.Property(e => e.OrerStart).IsRequired();
                entity.Property(e => e.OrerEnd).IsRequired();
                entity.Property(e => e.ChiefStart).IsRequired();
                entity.Property(e => e.ChiefEnd).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(e => e.Driver)
                    .WithMany()
                    .HasForeignKey(e => e.DriverId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.Vehicle)
                    .WithMany()
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.Route)
                    .WithMany()
                    .HasForeignKey(e => e.RouteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.HasOne(e => e.LineCode)
                    .WithMany()
                    .HasForeignKey(e => e.LineCodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });
        }
    }
}
