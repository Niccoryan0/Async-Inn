using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Layout FloorPlan { get; set; }

        // Nav Properties
        public List<HotelRoom> Hotels { get; set; }

        public List<RoomAmenity> Amenities { get; set; }
    }

    public enum Layout
    {
        Studio,
        OneBedroom,
        TwoBedrooms,
        ThreeBedrooms,
        Luxury
    }
}
