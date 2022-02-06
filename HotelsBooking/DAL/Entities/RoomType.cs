using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class RoomType
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }
    }
}
