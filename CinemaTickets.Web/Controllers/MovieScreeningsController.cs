using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTickets.Service.Interface;
using CinemaTickets.Domain.DomainModels;

namespace CinemaTickets.Web.Controllers
{
    public class MovieScreeningsController : Controller
    {
        private readonly IMovieScreeningService _movieScreeningService;
        private readonly IMovieService _movieService;
        public MovieScreeningsController(IMovieScreeningService movieScreeningService,
            IMovieService movieService)
        {
            this._movieScreeningService = movieScreeningService;
            this._movieService = movieService;
        }

        // GET: MovieScreenings
        public IActionResult Index()
        {
            var applicationDbContext = this._movieScreeningService.GetAllMovieScreenings();
            return View(applicationDbContext);
        }

        // GET: MovieScreenings/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = this._movieScreeningService.GetMovieScreeningById(id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // GET: MovieScreenings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(this._movieService.GetAllMovies(), "Id", "MovieName");
            return View();
        }

        // POST: MovieScreenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieId,DateAndTime,TicketPrice,Id")] MovieScreening movieScreening)
        {
            if (ModelState.IsValid)
            {
                this._movieScreeningService.CreateNewMovieScreening(movieScreening);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(this._movieService.GetAllMovies(), "Id", "MovieName", movieScreening.MovieId);
            return View(movieScreening);
        }

        // GET: MovieScreenings/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = this._movieScreeningService.GetMovieScreeningById(id);
            if (movieScreening == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(this._movieService.GetAllMovies(), "Id", "MovieDescription", movieScreening.MovieId);
            return View(movieScreening);
        }

        // POST: MovieScreenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("MovieId,DateAndTime,TicketPrice,Id")] MovieScreening movieScreening)
        {
            if (id != movieScreening.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._movieScreeningService.UpdateExistingMovieScreening(movieScreening);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieScreeningExists(movieScreening.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(this._movieService.GetAllMovies(), "Id", "MovieDescription", movieScreening.MovieId);
            return View(movieScreening);
        }

        // GET: MovieScreenings/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieScreening = this._movieScreeningService.GetMovieScreeningById(id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // POST: MovieScreenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var movieScreening = this._movieScreeningService.GetMovieScreeningById(id);
            if (movieScreening != null)
            {
                this._movieScreeningService.DeleteMovieScreening(movieScreening);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool MovieScreeningExists(Guid id)
        {
            return this._movieScreeningService.MovieScreeningExists(id);
        }
    }
}
