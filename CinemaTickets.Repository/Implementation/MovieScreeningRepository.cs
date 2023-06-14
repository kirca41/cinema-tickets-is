using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repository.Implementation
{
    public class MovieScreeningRepository : IMovieScreeningRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MovieScreening> entities;

        public MovieScreeningRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<MovieScreening>();
        }

        public void Delete(MovieScreening entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MovieScreening Get(Guid? id)
        {
            return entities.Include(m => m.Movie).SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<MovieScreening> GetAll()
        {
            return this.entities
                .Include(m => m.Movie)
                .ToListAsync()
                .Result;
        }

        public void Insert(MovieScreening entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MovieScreening entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public IEnumerable<MovieScreening> GetBefore(DateTime dateTime)
        {
            return this.entities
                .Where(m => m.DateAndTime <= dateTime)
                .Include(m => m.Movie)                
                .ToListAsync()
                .Result;
        }
    }
}
