using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public string MovieDescription { get; set; }

        public string Genre { get; set; }
    }
}
