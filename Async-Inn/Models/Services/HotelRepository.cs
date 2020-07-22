using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;
        
        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
            // Old way to add stuff to database:
            //_context.Hotels.Add(hotel);
            //_context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel result = await _context.Hotels.FindAsync(id);
            return result;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            List<Hotel> result = await _context.Hotels.ToListAsync();
            return result;
        }

        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
