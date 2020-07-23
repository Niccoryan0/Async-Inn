using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class RoomRepository : IRoom
    {
        private AsyncInnDbContext _context;

        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new room in the database
        /// </summary>
        /// <param name="room">Room to be added to database</param>
        /// <returns>Successful result of adding the room</returns>
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
            // Old way to add stuff to database:
            //_context.Rooms.Add(room);
            //_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a specific room from the database
        /// </summary>
        /// <param name="id">Id of room to be deleted</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific room from the database
        /// </summary>
        /// <param name="id">Id for room to be retrieved</param>
        /// <returns>Successful result of specified room</returns>
        public async Task<Room> GetRoom(int id)
        {
            var result = await _context.Rooms.FindAsync(id);

            // Include all amenities in room
            var amenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                        .Include(x => x.Amenity)
                                                        .ToListAsync();
            result.Amenities = amenities;
            return result;
        }

        /// <summary>
        /// Returns all rooms in database
        /// </summary>
        /// <returns>Successful result of list of rooms</returns>
        public async Task<List<Room>> GetRooms()
        {
            List<Room> result = await _context.Rooms.ToListAsync();
            return result;
        }

        /// <summary>
        /// Updates the details of a given room
        /// </summary>
        /// <param name="room">Room to be updated</param>
        /// <returns>Successful result of updated room</returns>
        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Adds a given amenity to a specific room
        /// </summary>
        /// <param name="amenityId">Unique id for amenity</param>
        /// <param name="roomId">Unique id for room</param>
        /// <returns>Task of completion</returns>
        public async Task AddAmenity(int amenityId, int roomId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes a given amenity from a specific room.
        /// </summary>
        /// <param name="roomId">Unique id for room</param>
        /// <param name="amenityId">Unique id for amenity to be removed</param>
        /// <returns>Task of completion</returns>
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result = _context.RoomAmenities.FirstOrDefaultAsync(x => x.RoomId == roomId && x.AmenityId == amenityId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
