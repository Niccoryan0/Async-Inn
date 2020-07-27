using Async_Inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// Creates a new amenity in the database
        /// </summary>
        /// <param name="amenity">Hotel to be added to database</param>
        /// <returns>Successful result of adding the amenity</returns>
        Task<AmenityDTO> Create(AmenityDTO amenity);


        /// <summary>
        /// Returns all amenities in database
        /// </summary>
        /// <returns>Successful result of List of amenities</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Gets a specific amenity from the database
        /// </summary>
        /// <param name="id">Id for amenity to be retrieved</param>
        /// <returns>Successful result of specified amenity</returns>
        Task<AmenityDTO> GetAmenity(int id);

        /// <summary>
        /// Updates the details of a given amenity
        /// </summary>
        /// <param name="amenity">Hotel to be updated</param>
        /// <returns>Successful result of updated amenity</returns>
        Task<Amenity> Update(Amenity amenity);

        /// <summary>
        /// Deletes a specific amenity from the database
        /// </summary>
        /// <param name="id">Id of amenity to be deleted</param>
        /// <returns>Task of completion</returns>
        Task Delete(int id);
    }
}
