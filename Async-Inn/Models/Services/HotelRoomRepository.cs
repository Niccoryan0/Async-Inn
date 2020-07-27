using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a room to a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="hotelRoom">HotelRoom object with data for room addition</param>
        /// <returns>Details of hotel room</returns>
        public async Task<HotelRoom> Create(int hotelId, HotelRoom hotelRoom)
        {
            hotelRoom.HotelId = hotelId;
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Delete a specific room from a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number in hotel</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a specific room in a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number to be retrieved</param>
        /// <returns>HotelRoom data for specified room</returns>
        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            //HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);

            var hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                .Include(x => x.Hotel)
                                                .Include(x => x.Room)
                                                .ThenInclude(x => x.Amenities)
                                                .ThenInclude(x => x.Amenity)
                                                .FirstOrDefaultAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Get all rooms in a certain hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <returns>List of all rooms in the hotel</returns>
        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> result = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                              .Include(x => x.Room)
                                                              .ToListAsync();
            return result;
        }

        /// <summary>
        /// Change information about a certain hotelRoom
        /// </summary>
        /// <param name="hotelRoom">Changed HotelRoom data</param>
        /// <returns>Changed HotelRoom data</returns>
        public async Task<HotelRoom> Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }
    }
}
