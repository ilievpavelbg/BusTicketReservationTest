using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusTicketReservationTest.Models
{
    public partial class TicketBookingTestContext : DbContext
    {
        public TicketBookingTestContext()
        {
        }

        public TicketBookingTestContext(DbContextOptions<TicketBookingTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BusOwner> BusOwners { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<bus> Buses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=TicketBookingTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Bookings_CustomerId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<BusOwner>(entity =>
            {
                entity.Property(e => e.Vatnumber).HasColumnName("VATnumber");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.HasMany(d => d.Schedules)
                    .WithMany(p => p.Destinations)
                    .UsingEntity<Dictionary<string, object>>(
                        "DestinationSchedule",
                        l => l.HasOne<Schedule>().WithMany().HasForeignKey("SchedulesId"),
                        r => r.HasOne<Destination>().WithMany().HasForeignKey("DestinationsId"),
                        j =>
                        {
                            j.HasKey("DestinationsId", "SchedulesId");

                            j.ToTable("DestinationSchedule");

                            j.HasIndex(new[] { "SchedulesId" }, "IX_DestinationSchedule_SchedulesId");
                        });
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasIndex(e => e.BookingId, "IX_Tickets_BookingId");

                entity.HasIndex(e => e.BusId, "IX_Tickets_BusId");

                entity.HasIndex(e => e.DestinationId, "IX_Tickets_DestinationId");

                entity.Property(e => e.IsValid)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.BookingId);

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.BusId);

                entity.HasOne(d => d.Destination)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.DestinationId);
            });

            modelBuilder.Entity<bus>(entity =>
            {
                entity.HasIndex(e => e.BusOwnerId, "IX_Buses_BusOwnerId");

                entity.HasOne(d => d.BusOwner)
                    .WithMany(p => p.buses)
                    .HasForeignKey(d => d.BusOwnerId);

                entity.HasMany(d => d.Seats)
                    .WithMany(p => p.Buses)
                    .UsingEntity<Dictionary<string, object>>(
                        "BusSeat",
                        l => l.HasOne<Seat>().WithMany().HasForeignKey("SeatsId"),
                        r => r.HasOne<bus>().WithMany().HasForeignKey("BusesId"),
                        j =>
                        {
                            j.HasKey("BusesId", "SeatsId");

                            j.ToTable("BusSeat");

                            j.HasIndex(new[] { "SeatsId" }, "IX_BusSeat_SeatsId");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
