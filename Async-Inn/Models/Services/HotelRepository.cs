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
        private readonly AsyncInnDbContext _context;
        private readonly IHotelRoom _hotelRooms;

        public HotelRepository(AsyncInnDbContext context, IHotelRoom hotelRooms)
        {
            _context = context;
            _hotelRooms = hotelRooms;
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
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific hotel from the database
        /// </summary>
        /// <param name="id">Id for hotel to be retrieved</param>
        /// <returns>Successful result of specified hotel</returns>
        public async Task<HotelDTO> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Where(x => x.Id == id)
                                 .Include(x => x.Rooms)
                                 .FirstOrDefaultAsync();
            HotelDTO hotelDTO = new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };
            hotelDTO.Rooms = new List<HotelRoomDTO>();
            foreach (var room in hotel.Rooms)
            {
                hotelDTO.Rooms.Add(await _hotelRooms.GetHotelRoom(room.HotelId, room.RoomNumber));
            }
            return hotelDTO;
        }

        /// <summary>
        /// Returns all hotels in database
        /// </summary>
        /// <returns>Successful result of List of hotels</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            List<Hotel> result = await _context.Hotels.ToListAsync();
            List<HotelDTO> hotels = new List<HotelDTO>();
            foreach (var item in result)
            {
                hotels.Add(await GetHotel(item.Id));
            }
            return hotels;
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
