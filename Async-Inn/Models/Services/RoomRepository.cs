using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class RoomRepository : IRoom
    {
        private AsyncInnDbContext _context;
        private IAmenity _amenities;

        public RoomRepository(AsyncInnDbContext context, IAmenity amenity)
        {
            _context = context;
            _amenities = amenity;
        }

        /// <summary>
        /// Creates a new room in the database
        /// </summary>
        /// <param name="room">Room to be added to database</param>
        /// <returns>Successful result of adding the room</returns>
        public async Task<RoomDTO> Create(RoomDTO roomDTO)
        {
            Enum.TryParse(roomDTO.Layout, out Layout layout);
            Room room = new Room
            {
                Id = roomDTO.ID,
                Name = roomDTO.Name,
                FloorPlan = layout
            };
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return roomDTO;
        }

        /// <summary>
        /// Deletes a specific room from the database
        /// </summary>
        /// <param name="id">Id of room to be deleted</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific room from the database
        /// </summary>
        /// <param name="id">Id for room to be retrieved</param>
        /// <returns>Successful result of specified room</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            // Include all amenities in room
            List<Amenity> roomAmenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                             .Select(room => room.Amenity)
                                             .ToListAsync();
            //_amenities.GetAmenities(id)
            List<AmenityDTO> amenities = new List<AmenityDTO>();
            foreach (var amenity in roomAmenities)
            {
                amenities.Add(await _amenities.GetAmenity(amenity.Id));
            }
            // Convert room to RoomDTO
            RoomDTO newRoom = new RoomDTO
            {
                ID = room.Id,
                Name = room.Name,
                Layout = room.FloorPlan.ToString(),
                Amenities = amenities
            };
            return newRoom;
        }

        /// <summary>
        /// Returns all rooms in database
        /// </summary>
        /// <returns>Successful result of List of rooms</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            //var result = await _context.Rooms.Include(room => room.Hotels)
            //                                 .ThenInclude(hotels => hotels.Hotel)
            //                                 .Include(room => room.Amenities)
            //                                 .ThenInclude(x => x.Amenity)
            //                                 .ToListAsync();
            List<Room> result = await _context.Rooms.ToListAsync();
            var rooms = new List<RoomDTO>();
            foreach (var room in result)
            {
                rooms.Add(await GetRoom(room.Id));
            }
            return rooms;
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
