using Async_Inn.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// Creates a new hotel in the database from a Hotel DTO
        /// </summary>
        /// <param name="hotel">HotelDTO to be added to database</param>
        /// <returns>Successful result of adding the hotel</returns>
        Task<HotelDTO> Create(HotelDTO hotelDTO);

        /// <summary>
        /// Returns all hotels in database
        /// </summary>
        /// <returns>Successful result of List of hotelDTOs</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// Gets a specific hotel from the database
        /// </summary>
        /// <param name="id">Id for hotel to be retrieved</param>
        /// <returns>Successful result of specified hotelDTO</returns>
        Task<HotelDTO> GetHotel(int id);

        /// <summary>
        /// Updates the details of a given hotel
        /// </summary>
        /// <param name="hotel">Hotel to be updated</param>
        /// <returns>Successful result of updated hotel</returns>
        Task<Hotel> Update(Hotel hotel);

        /// <summary>
        /// Deletes a specific hotel from the database
        /// </summary>
        /// <param name="id">Id of hotel to be deleted</param>
        /// <returns>Task of completion</returns>
        Task Delete(int id);

        /// <summary>
        /// Adds a room to a given hotel
        /// </summary>
        /// <param name="hotelId">Unique id of hotel</param>
        /// <param name="roomNumber">Number of room in hotel</param>
        /// <param name="roomId">Unique Id of room</param>
        /// <param name="petFriendly">Bool representing if room is pet friendly</param>
        /// <param name="rate">Nightly rate of room</param>
        /// <returns>Task of Completion</returns>
        Task AddRoom(int hotelId, int roomNumber, int roomId, bool petFriendly, decimal rate);
    }
}