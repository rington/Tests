using System.Collections.Generic;

namespace BLL.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PassportId { get; set; }

        public ICollection<ReservationDTO> Reservation { get; set; }
    }
}
