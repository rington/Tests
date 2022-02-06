using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Hotel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public string Rating { get; set; }
    }
}
