using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
