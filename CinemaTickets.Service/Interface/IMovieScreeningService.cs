using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Interface
{
    public interface IMovieScreeningService
    {
        List<MovieScreening> GetAllMovieScreenings();

        MovieScreening GetMovieScreeningById(Guid? id);

        void CreateNewMovieScreening(MovieScreening movieScreening);

        void UpdateExistingMovieScreening(MovieScreening movieScreening);

        void DeleteMovieScreening(MovieScreening movieScreening);

        bool MovieScreeningExists(Guid id);

        List<MovieScreening> GetMovieScreeningsBefore(DateTime dateTime);
    }
}
