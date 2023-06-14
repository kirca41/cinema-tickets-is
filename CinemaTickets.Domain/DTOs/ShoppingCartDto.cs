using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DTOs
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }

        public int TotalPrice { get; set; }
    }
}
