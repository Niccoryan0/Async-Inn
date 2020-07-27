using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class RoomAmenity
    {
        // Composite key is these two together:
        public int RoomId { get; set; }
        public int AmenityId { get; set; }


        public Amenity Amenity { get; set; }
        public Room Room { get; set; }
    }
}
