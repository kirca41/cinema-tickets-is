using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Web.Models.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public string MovieDescription { get; set; }

    }
}
