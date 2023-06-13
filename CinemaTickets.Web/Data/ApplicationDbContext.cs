using CinemaTickets.Web.Models.DomainModels;
using CinemaTickets.Web.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<CinemaTicketsApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieScreening> MovieScreenings { get; set; }
    }
}