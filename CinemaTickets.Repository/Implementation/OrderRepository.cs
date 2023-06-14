using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaTickets.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return this.entities
                .Include(z => z.Tickets)
                .Include("Tickets.MovieScreening")
                .Include("Tickets.MovieScreening.Movie")
                .Include(z => z.User)
                .ToListAsync().Result;
        }

        public List<Order> GetAllOrdersByUser(string userId)
        {
            return this.entities
                .Include(z => z.Tickets)
                .Include("Tickets.MovieScreening")
                .Include("Tickets.MovieScreening.Movie")
                .Where(z => z.UserId.Equals(userId))
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(Guid id)
        {
            return this.entities
                .Include(z => z.Tickets)
                .Include("Tickets.MovieScreening")
                .Include("Tickets.MovieScreening.Movie")
                .Include(z => z.User)
                .SingleOrDefault(z => z.Id.Equals(id));
        }

        public void PlaceOrder(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
    }
}
