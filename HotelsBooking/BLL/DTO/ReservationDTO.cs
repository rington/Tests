using System;

namespace BLL.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int RoomId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime BookingDateEnd { get; set; }
    }
}
