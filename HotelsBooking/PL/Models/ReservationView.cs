using System;

namespace PL.Models
{
    public class ReservationView
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int RoomId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime BookingDateEnd { get; set; }
    }
}