using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class TicketInShoppingCart : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid MovieScreeningId { get; set; }
        public MovieScreening MovieScreening { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
