using CinemaTickets.Domain.Identity;
using CinemaTickets.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CinemaTicketsApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CinemaTicketsApplicationUser>();
        }
        public IEnumerable<CinemaTicketsApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public CinemaTicketsApplicationUser Get(string id)
        {
            return entities
                .Include(z => z.ShoppingCart)
                // .Include("UserCart.ProductsInShoppingCart")
                // .Include("UserCart.ProductsInShoppingCart.Product")
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(CinemaTicketsApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(CinemaTicketsApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(CinemaTicketsApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
