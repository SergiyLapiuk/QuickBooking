using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace QuickBooking.Models
{
    public class QuickBookingDbContext : DbContext
    {
        public QuickBookingDbContext(DbContextOptions<QuickBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Bookings)
                      .WithOne(b => b.User)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasIndex(h => h.Location)
                      .IsUnique()
                      .HasDatabaseName("IX_Hotels_Location");

                entity.HasMany(h => h.HotelRooms)
                      .WithOne(hr => hr.Hotel)
                      .HasForeignKey(hr => hr.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.HasIndex(hr => new { hr.HotelId, hr.RoomNumber })
                      .IsUnique()
                      .HasDatabaseName("IX_HotelRooms_HotelId_RoomNumber");
                
                
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.HasMany(rt => rt.HotelRooms)
                      .WithOne(hr => hr.RoomType)
                      .HasForeignKey(hr => hr.RoomTypeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(rt => rt.TypeName)
                      .IsUnique()
                      .HasDatabaseName("IX_RoomTypes_TypeName");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasOne(t => t.HotelRoom)
                      .WithMany()
                      .HasForeignKey(t => t.HotelRoomId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(t => t.Transfers)
                      .WithOne(tr => tr.Tour)
                      .HasForeignKey(tr => tr.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TransferType>(entity =>
            {
                entity.HasMany(tt => tt.Transfers)
                      .WithOne(tr => tr.TransferType)
                      .HasForeignKey(tr => tr.TransferTypeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasOne(b => b.User)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Tour)
                      .WithMany()
                      .HasForeignKey(b => b.TourId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
