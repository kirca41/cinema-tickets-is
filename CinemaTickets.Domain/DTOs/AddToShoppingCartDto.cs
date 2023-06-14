using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DTOs
{
    public class AddToShoppingCartDto
    {
        public Guid MovieScreeningId { get; set; }
        public MovieScreening MovieScreening { get; set; }
        public int Quantity { get; set; }

    }
}
