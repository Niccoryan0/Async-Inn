﻿using Async_Inn.Data;
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
        private readonly AsyncInnDbContext _context;
        private readonly IAmenity _amenities;

        public RoomRepository(AsyncInnDbContext context, IAmenity amenity)
        {
            _context = context;
            _amenities = amenity;
        }

        /// <summary>
        /// Creates a new room in the database
        /// </summary>
        /// <param name="room">Room to be added to database as DTO</param>
        /// <returns>Successful result of adding the roomDTO</returns>
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
        /// <returns>Successful result of specified roomDTO</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.Where(x => x.Id == id)
                                             .Include(x => x.Amenities)
                                             .FirstOrDefaultAsync();

            RoomDTO newRoom = new RoomDTO
            {
                ID = room.Id,
                Name = room.Name,
                Layout = room.FloorPlan.ToString(),
            };
            newRoom.Amenities = new List<AmenityDTO>();
            foreach (var amenity in room.Amenities)
            {
                newRoom.Amenities.Add(await _amenities.GetAmenity(amenity.AmenityId));
            }
            return newRoom;
        }

        /// <summary>
        /// Returns all rooms in database
        /// </summary>
        /// <returns>Successful result of List of roomDTOs</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            List<Room> result = await _context.Rooms.ToListAsync();
            List<RoomDTO> rooms = new List<RoomDTO>();
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
        /// <returns>Successful result of updated roomDTO</returns>
        public async Task<RoomDTO> Update(int id, RoomDTO roomDTO)
        {
            Enum.TryParse(roomDTO.Layout, out Layout layout);
            Room room = new Room
            {
                Id = id,
                Name = roomDTO.Name,
                FloorPlan = layout
            };
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDTO;
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
            RoomAmenity result = await _context.RoomAmenities.FindAsync(roomId, amenityId);

            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
