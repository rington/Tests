using System.Data.Entity;
using DAL.Entities;

namespace DAL.EFContext
{
    public class HotelContext : DbContext
    {
        public HotelContext() : base("MyHotel")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
