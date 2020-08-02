using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Controllers
{
    [Route("api/Hotels")]
    [Authorize(Policy = "HigherUps")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom context)
        {
            _hotelRoom = context;
        }

        // GET: api/HotelRooms
        [HttpGet("{hotelId}/Rooms")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms(int hotelId)
        {
            return await _hotelRoom.GetHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/Rooms/{roomNumber}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return hotelRoom;
        }

        // PUT: api/Hotels/1/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{hotelId}/Rooms/{roomNumber}")]
        [Authorize(Policy = "AllEmployees")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            if (hotelId != hotelRoomDTO.HotelID || roomNumber != hotelRoomDTO.RoomNumber)
            {
                return BadRequest();
            }

            var updatedHotel = await _hotelRoom.Update(hotelId, roomNumber, hotelRoomDTO);

            return Ok(updatedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            await _hotelRoom.Create(hotelId, hotelRoomDTO);
            return CreatedAtAction("GetHotelRoom", new { hotelId = hotelRoomDTO.HotelID }, hotelRoomDTO);
        }

        // DELETE: api/Hotels
        [HttpDelete("{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);
            return NoContent();
        }
    }
}