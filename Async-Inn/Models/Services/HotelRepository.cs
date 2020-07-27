using Async_Inn.Controllers;
using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;
        
        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new hotel in the database
        /// </summary>
        /// <param name="hotel">Hotel to be added to database</param>
        /// <returns>Successful result of adding the hotel</returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
            // Old way to add stuff to database:
            //_context.Hotels.Add(hotel);
            //_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a specific hotel from the database
        /// </summary>
        /// <param name="id">Id of hotel to be deleted</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific hotel from the database
        /// </summary>
        /// <param name="id">Id for hotel to be retrieved</param>
        /// <returns>Successful result of specified hotel</returns>
        public async Task<Hotel> GetHotel(int id)
        {
            Hotel result = await _context.Hotels.FindAsync(id);
            var rooms = await _context.HotelRooms.Where(x => x.HotelId == id)
                                                 .Include(hotelRoom => hotelRoom.Room)
                                                 .ThenInclude(room => room.Amenities)
                                                 .ThenInclude(amenities => amenities.Amenity)
                                                 .ToListAsync();
            HotelDTO hotelDTO = new HotelDTO
            {
                ID = result.Id,
                Name = result.Name,
                StreetAddress = result.StreetAddress,
                City = result.City,
                State = result.State,
                Phone = result.Phone
            };

            result.Rooms = rooms;
            return result;
        }

        /// <summary>
        /// Returns all hotels in database
        /// </summary>
        /// <returns>Successful result of List of hotels</returns>
        public async Task<List<Hotel>> GetHotels()
        {
            List<Hotel> result = await _context.Hotels.ToListAsync();
            return result;
        }

        /// <summary>
        /// Updates the details of a given hotel
        /// </summary>
        /// <param name="hotel">Hotel to be updated</param>
        /// <returns>Successful result of updated hotel</returns>
        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Adds a given room to a specific hotel
        /// </summary>
        /// <param name="roomId">Unique id for room</param>
        /// <param name="hotelId">Unique id for hotel</param>
        /// <returns>Task of completion</returns>
        public async Task AddRoom(int hotelId, int roomNumber, int roomId, bool petFriendly, decimal rate)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                HotelId = hotelId,
                RoomNumber = roomNumber,
                RoomId = roomId,
                PetFriendly = petFriendly,
                Rate = rate
            };
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
    }
}
