using CinemaTickets.Domain.Identity;

namespace CinemaTickets.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaTicketsApplicationUser User { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }

    }
}
