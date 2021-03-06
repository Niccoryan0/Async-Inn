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
    public class AmenityRepository : IAmenity
    {
        private readonly AsyncInnDbContext _context;

        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new amenity in the database
        /// </summary>
        /// <param name="amenity">Amenity to be added to database as DTO</param>
        /// <returns>Successful result of adding the amenityDTO</returns>
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            Amenity newAmenity = new Amenity() 
            {
                Name = amenity.Name
            };

            _context.Entry(newAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            amenity.ID = newAmenity.Id;
            return amenity;
        }

        /// <summary>
        /// Deletes a specific amenity from the database
        /// </summary>
        /// <param name="id">Id of amenity to be deleted</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific amenity DTO from the database
        /// </summary>
        /// <param name="id">Id for amenity to be retrieved</param>
        /// <returns>Successful result of specified amenity DTO</returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            var result = await _context.Amenities.FindAsync(id);
            AmenityDTO amenityDTO = new AmenityDTO
            {
                ID = result.Id,
                Name = result.Name
            };
            return amenityDTO;
        }

        /// <summary>
        /// Returns all amenities in database
        /// </summary>
        /// <returns>Successful result of List of amenities as DTOs</returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            List<Amenity> result = await _context.Amenities.ToListAsync();
            var amenities = new List<AmenityDTO>();

            foreach (var amenity in result)
            {
                amenities.Add(await GetAmenity(amenity.Id));
            }
            return amenities;
        }

        /// <summary>
        /// Updates the details of a given amenity
        /// </summary>
        /// <param name="amenity">Amenity DTO to be updated</param>
        /// <returns>Successful result of updated amenity</returns>
        public async Task<AmenityDTO> Update(AmenityDTO amenityDTO)
        {
            Amenity amenity = new Amenity
            {
                Id = amenityDTO.ID,
                Name = amenityDTO.Name
            };
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenityDTO;
        }
    }
}
