using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class HotelRoom
    {
        // Composite key is these two together:
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }


        public int RoomId { get; set; }
        public bool PetFriendly { get; set; }
        public decimal Rate { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }


    }
}
