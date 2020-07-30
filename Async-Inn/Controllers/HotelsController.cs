using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "HigherUps")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            return await _hotel.GetHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
          return await _hotel.GetHotel(id);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "DistrictManagerOnly")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var updatedHotel = await _hotel.Update(hotel);

            return Ok(updatedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "DistrictManagerOnly")]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotelDTO)
        {
            await _hotel.Create(hotelDTO);
            return CreatedAtAction("GetHotel", new { id = hotelDTO.ID }, hotelDTO);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "DistrictManagerOnly")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _hotel.Delete(id);
            return NoContent();
        }
    }
}
