using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sets combination composite key
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });
            modelBuilder.Entity<RoomAmenity>().HasKey(x => new { x.RoomId, x.AmenityId });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Dummy Hotel",
                    StreetAddress = "123 Dummy St.",
                    City = "Seattle",
                    State = "Wa",
                    Phone = "3606655432"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Dummy Hotel v2",
                    StreetAddress = "123456 Dummy St.",
                    City = "Redmond",
                    State = "Wash",
                    Phone = "3606332332"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Dummy Hotel v3",
                    StreetAddress = "123456789 Dummy St.",
                    City = "Everett",
                    State = "Washington",
                    Phone = "36066553231222"
                }
                );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Dummy Room",
                    FloorPlan = Layout.Studio
                },
                new Room
                {
                    Id = 2,
                    Name = "Dummy Room v2",
                    FloorPlan = Layout.TwoBedrooms

                },
                new Room
                {
                    Id = 3,
                    Name = "Dummy Room v3",
                    FloorPlan = Layout.Luxury
                }
                );
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "A/C"
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Flatscreen TV"

                },
                new Amenity
                {
                    Id = 3,
                    Name = "Big ol shower"
                }
                ); 
        }


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }


    }
}
