using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        /// <summary>
        /// Add a room to a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="hotelRoom">HotelRoom object with data for room addition</param>
        /// <returns>Details of hotel room</returns>
        Task<HotelRoomDTO> Create(int hotelId, HotelRoomDTO hotelRoomDTO);

        /// <summary>
        /// Get all rooms in a certain hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <returns>List of all rooms in the hotel</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);
        
        /// <summary>
        /// Get a specific room in a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number to be retrieved</param>
        /// <returns>HotelRoom data for specified room</returns>
        Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber);

        /// <summary>
        /// Change information about a certain hotelRoom
        /// </summary>
        /// <param name="hotelRoom">Changed HotelRoom data</param>
        /// <returns>Changed HotelRoom data</returns>
        Task<HotelRoom> Update(HotelRoom hotelRoom);

        /// <summary>
        /// Delete a specific room from a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number in hotel</param>
        /// <returns>Task of completion</returns>
        Task Delete(int hotelId, int roomNumber);
    }
}
