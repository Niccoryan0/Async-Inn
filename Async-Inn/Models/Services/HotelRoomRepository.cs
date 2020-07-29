using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;
        private readonly IRoom _rooms;

        public HotelRoomRepository(AsyncInnDbContext context, IRoom rooms)
        {
            _context = context;
            _rooms = rooms;
        }

        /// <summary>
        /// Add a room to a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="hotelRoom">HotelRoomDTO object with data for room addition</param>
        /// <returns>Details of hotel room as DTO</returns>
        public async Task<HotelRoomDTO> Create(int hotelId, HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = hotelRoomDTO.RoomNumber,
                Rate = hotelRoomDTO.Rate,
                PetFriendly = hotelRoomDTO.PetFriendly,
                RoomId = hotelRoomDTO.RoomID
            };
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoomDTO;
        }

        /// <summary>
        /// Delete a specific room from a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number in hotel</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get a specific room in a hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <param name="roomNumber">Room number to be retrieved</param>
        /// <returns>HotelRoomDTO data for specified room</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            var roomId = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                           .Select(hotelRoom => hotelRoom.RoomId)
                                           .FirstOrDefaultAsync();
            var room = await _rooms.GetRoom(roomId);


            HotelRoomDTO hotelRoomDTO = new HotelRoomDTO
            {
                HotelID = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomID = hotelRoom.RoomId,
                Room = room
            };
            return hotelRoomDTO;
        }

        /// <summary>
        /// Get all rooms in a certain hotel
        /// </summary>
        /// <param name="hotelId">Unique ID of hotel</param>
        /// <returns>List of all rooms in the hotel as DTOs</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> result = await _context.HotelRooms.Where(x => x.HotelId == hotelId).ToListAsync();
            var hotelRooms = new List<HotelRoomDTO>();
            foreach (var room in result)
            {
                hotelRooms.Add(await GetHotelRoom(room.HotelId, room.RoomNumber));
            }
            return hotelRooms;
        }

        /// <summary>
        /// Change information about a certain hotelRoom
        /// </summary>
        /// <param name="hotelRoom">Updated HotelRoom information as DTO</param>
        /// <returns>Changed HotelRoomDTO data</returns>
        public async Task<HotelRoomDTO> Update(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = roomNumber,
                RoomId = hotelRoomDTO.RoomID,
                PetFriendly = hotelRoomDTO.PetFriendly,
                Rate = hotelRoomDTO.Rate
            };
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoomDTO;
        }
    }
}
