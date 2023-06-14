using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repository.Interface
{
    public interface IMovieScreeningRepository
    {
        IEnumerable<MovieScreening> GetAll();
        MovieScreening Get(Guid? id);
        void Insert(MovieScreening entity);
        void Update(MovieScreening entity);
        void Delete(MovieScreening entity);
    }
}
