using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Web.Models.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
