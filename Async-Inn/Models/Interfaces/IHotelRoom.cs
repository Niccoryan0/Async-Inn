using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(int hotelId, HotelRoom hotelRoom);

        Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);

        Task<HotelRoom> Update(HotelRoom hotelRoom);

        Task Delete(int hotelId, int roomNumber);
    }
}
