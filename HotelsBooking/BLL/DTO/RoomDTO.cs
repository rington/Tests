using System.Collections.Generic;
using DAL.Entities;

namespace BLL.DTO
{
   public class RoomDTO
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }

        public int HotelId { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
