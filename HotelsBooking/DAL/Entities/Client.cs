using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(8)]
        public string PassportId { get; set; }

        public  IEnumerable<Reservation> Reservations { get; set; }
    }
}
