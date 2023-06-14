using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;

namespace CinemaTickets.Service.Implementation
{
    public class MovieService : IMovieService
    {

        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            this._movieRepository = movieRepository;
        }

        public void CreateNewMovie(Movie movie)
        {
            movie.Id = Guid.NewGuid();
            this._movieRepository.Insert(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            this._movieRepository.Delete(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return this._movieRepository.GetAll().ToList();
        }

        public Movie GetMovieById(Guid? id)
        {
            return this._movieRepository.Get(id);
        }

        public bool MovieExists(Guid id)
        {
            return this._movieRepository.Get(id) != null;
        }

        public void UpdateExistingMovie(Movie movie)
        {
            this._movieRepository.Update(movie);
        }
    }
}
