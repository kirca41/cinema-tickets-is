using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();

        Movie GetMovieById(Guid? id);

        void CreateNewMovie(Movie movie);

        void UpdateExistingMovie(Movie movie);

        void DeleteMovie(Movie movie);

        bool MovieExists(Guid id);
    }
}
