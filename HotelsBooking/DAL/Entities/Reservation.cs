using System;

namespace DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime BookingDateEnd { get; set; }
    }
}
