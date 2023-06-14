using CinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CinemaTicketsApplicationUser> GetAll();
        CinemaTicketsApplicationUser Get(string id);
        void Insert(CinemaTicketsApplicationUser entity);
        void Update(CinemaTicketsApplicationUser entity);
        void Delete(CinemaTicketsApplicationUser entity);
    }
}
