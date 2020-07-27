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
        /// <param name="room">Room to be added to database</param>
        /// <returns>Successful result of adding the room</returns>
        Task<Room> Create(Room room);

        /// <summary>
        /// Returns all rooms in database
        /// </summary>
        /// <returns>Successful result of list of rooms</returns>
        Task<List<Room>> GetRooms();

        /// <summary>
        /// Gets a specific room from the database
        /// </summary>
        /// <param name="id">Id for room to be retrieved</param>
        /// <returns>Successful result of specified room</returns>
        Task<Room> GetRoom(int id);

        /// <summary>
        /// Updates the details of a given room
        /// </summary>
        /// <param name="room">Room to be updated</param>
        /// <returns>Successful result of updated room</returns>
        Task<Room> Update(Room room);

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
