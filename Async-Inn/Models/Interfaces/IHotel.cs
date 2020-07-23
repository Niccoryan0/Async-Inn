using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// Creates a new hotel in the database
        /// </summary>
        /// <param name="hotel">Hotel to be added to database</param>
        /// <returns>Successful result of adding the hotel</returns>
        Task<Hotel> Create(Hotel hotel);

        /// <summary>
        /// Returns all hotels in database
        /// </summary>
        /// <returns>Successful result of list of hotels</returns>
        Task<List<Hotel>> GetHotels();

        /// <summary>
        /// Gets a specific hotel from the database
        /// </summary>
        /// <param name="id">Id for hotel to be retrieved</param>
        /// <returns>Successful result of specified hotel</returns>
        Task<Hotel> GetHotel(int id);

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
        /// Adds a given room to a specific hotel
        /// </summary>
        /// <param name="roomId">Unique id for room</param>
        /// <param name="hotelId">Unique id for hotel</param>
        /// <returns>Task of completion</returns>
        Task AddRoom(int hotelId, int roomNumber, int roomId, bool petFriendly, decimal rate);
    }
}