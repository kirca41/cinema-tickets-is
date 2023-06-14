using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Implementation
{
    public class MovieScreeningService : IMovieScreeningService
    {
        private readonly IMovieScreeningRepository _movieScreeningRepository;

        public MovieScreeningService(IMovieScreeningRepository movieScreeningRepository)
        {
            this._movieScreeningRepository = movieScreeningRepository;
        }

        public void CreateNewMovieScreening(MovieScreening movieScreening)
        {
            movieScreening.Id = Guid.NewGuid();
            this._movieScreeningRepository.Insert(movieScreening);
        }

        public void DeleteMovieScreening(MovieScreening movieScreening)
        {
            this._movieScreeningRepository.Delete(movieScreening);
        }

        public List<MovieScreening> GetAllMovieScreenings()
        {
            return this._movieScreeningRepository.GetAll().ToList();
        }

        public MovieScreening GetMovieScreeningById(Guid? id)
        {
            return this._movieScreeningRepository.Get(id);
        }

        public List<MovieScreening> GetMovieScreeningsBefore(DateTime dateTime)
        {
            return this._movieScreeningRepository.GetBefore(dateTime).ToList();
        }

        public bool MovieScreeningExists(Guid id)
        {
            return this._movieScreeningRepository.Get(id) != null;
        }

        public void UpdateExistingMovieScreening(MovieScreening movieScreening)
        {
            this._movieScreeningRepository.Update(movieScreening);
        }
    }
}
