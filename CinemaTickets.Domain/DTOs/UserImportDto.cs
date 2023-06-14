using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DTOs
{
    public class UserImportDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
