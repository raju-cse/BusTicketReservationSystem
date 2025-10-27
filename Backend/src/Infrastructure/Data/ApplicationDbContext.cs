
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Bus> Buses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<BusSchedule> BusSchedules { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Passenger> Passengers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BusName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BusNumber).IsRequired().HasMaxLength(20);
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FromCity).IsRequired().HasMaxLength(50);
            entity.Property(e => e.ToCity).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<BusSchedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(bs => bs.Bus)
                  .WithMany(b => b.Schedules)
                  .HasForeignKey(bs => bs.BusId);

            entity.HasOne(bs => bs.Route)
                  .WithMany(r => r.Schedules)
                  .HasForeignKey(bs => bs.RouteId);

            entity.Property(e => e.ActualPrice).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(s => s.Bus)
                  .WithMany(b => b.Seats)
                  .HasForeignKey(s => s.BusId);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(t => t.BusSchedule)
                  .WithMany(bs => bs.Tickets)
                  .HasForeignKey(t => t.BusScheduleId);

            entity.HasOne(t => t.Seat)
                  .WithMany()
                  .HasForeignKey(t => t.SeatId);

            entity.HasOne(t => t.Passenger)
                  .WithMany(p => p.Tickets)
                  .HasForeignKey(t => t.PassengerId);

            entity.HasIndex(t => new { t.BusScheduleId, t.SeatId })
                  .IsUnique();
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.MobileNumber).IsRequired().HasMaxLength(15);
            entity.Property(e => e.PasswordHash).IsRequired(false);
            entity.HasIndex(e => e.MobileNumber).IsUnique();
        });
    }
}
