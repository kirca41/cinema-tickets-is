using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DTOs
{
    public class OrderDto
    {
        public Order Order { get; set; }

        public int TotalPrice { get; set; }
    }
}
