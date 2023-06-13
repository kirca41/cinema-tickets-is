using CinemaTickets.Web.Models.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace CinemaTickets.Web.Models.Identity
{
    public class CinemaTicketsApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
