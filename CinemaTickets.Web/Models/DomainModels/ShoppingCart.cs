using CinemaTickets.Web.Models.Identity;

namespace CinemaTickets.Web.Models.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaTicketsApplicationUser User { get; set; }
    }
}
