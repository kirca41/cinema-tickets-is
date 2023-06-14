using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class TicketInOrder : BaseEntity
    {
        public Guid MovieScreeningId { get; set; }

        public MovieScreening MovieScreening { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }

    }
}
