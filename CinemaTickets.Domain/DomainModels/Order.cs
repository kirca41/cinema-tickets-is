using CinemaTickets.Domain.Identity;

namespace CinemaTickets.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }

        public CinemaTicketsApplicationUser User { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public virtual ICollection<TicketInOrder> Tickets { get; set; }
    }
}
