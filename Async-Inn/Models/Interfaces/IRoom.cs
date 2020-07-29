using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a new room in the database
        /// </summary>
        /// <param name="room">Room to be added to database as DTO</param>
        /// <returns>Successful result of adding the roomDTO</returns>
        Task<RoomDTO> Create(RoomDTO roomDTO);

        /// <summary>
        /// Returns all rooms in database
        /// </summary>
        /// <returns>Successful result of List of roomDTOs</returns>
        Task<List<RoomDTO>> GetRooms();

        /// <summary>
        /// Gets a specific room from the database
        /// </summary>
        /// <param name="id">Id for room to be retrieved</param>
        /// <returns>Successful result of specified roomDTO</returns>
        Task<RoomDTO> GetRoom(int id);

        /// <summary>
        /// Updates the details of a given room
        /// </summary>
        /// <param name="room">Room to be updated</param>
        /// <returns>Successful result of updated roomDTO</returns>
        Task<RoomDTO> Update(int id, RoomDTO roomDTO);

        /// <summary>
        /// Deletes a specific room from the database
        /// </summary>
        /// <param name="id">Id of room to be deleted</param>
        /// <returns>Task of completion</returns>
        Task Delete(int id);

        /// <summary>
        /// Adds a given amenity to a specific room
        /// </summary>
        /// <param name="amenityId">Unique id for amenity</param>
        /// <param name="roomId">Unique id for room</param>
        /// <returns>Task of completion</returns>
        Task AddAmenity(int amenityId, int roomId);

        /// <summary>
        /// Removes a given amenity from a specific room.
        /// </summary>
        /// <param name="roomId">Unique id for room</param>
        /// <param name="amenityId">Unique id for amenity to be removed</param>
        /// <returns>Task of completion</returns>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
