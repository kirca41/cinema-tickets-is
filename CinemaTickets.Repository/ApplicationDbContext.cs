using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CinemaTicketsApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieScreening> MovieScreenings { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketInOrder> TicketsInOrder { get; set; }
    }
}