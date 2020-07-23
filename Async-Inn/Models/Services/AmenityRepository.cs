using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class AmenityRepository : IAmenity
    {
        private AsyncInnDbContext _context;

        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new amenity in the database
        /// </summary>
        /// <param name="amenity">Hotel to be added to database</param>
        /// <returns>Successful result of adding the amenity</returns>
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
            // Old way to add stuff to database:
            //_context.Amenities.Add(amenity);
            //_context.SaveChanges();
        }

        /// <summary>
        /// Deletes a specific amenity from the database
        /// </summary>
        /// <param name="id">Id of amenity to be deleted</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific amenity from the database
        /// </summary>
        /// <param name="id">Id for amenity to be retrieved</param>
        /// <returns>Successful result of specified amenity</returns>
        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity result = await _context.Amenities.FindAsync(id);
            var rooms = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                      .Include(x => x.Room)
                                                      .ToListAsync();
            result.Rooms = rooms;
            return result;
        }

        /// <summary>
        /// Returns all amenities in database
        /// </summary>
        /// <returns>Successful result of list of amenities</returns>
        public async Task<List<Amenity>> GetAmenities()
        {
            List<Amenity> result = await _context.Amenities.ToListAsync();
            return result;
        }

        /// <summary>
        /// Updates the details of a given amenity
        /// </summary>
        /// <param name="amenity">Hotel to be updated</param>
        /// <returns>Successful result of updated amenity</returns>
        public async Task<Amenity> Update(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
