using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTickets.Web.Data;
using CinemaTickets.Web.Models.DomainModels;

namespace CinemaTickets.Web.Controllers
{
    public class MovieScreeningsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieScreeningsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieScreenings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieScreenings.Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieScreenings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MovieScreenings == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.MovieScreenings
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // GET: MovieScreenings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName");
            return View();
        }

        // POST: MovieScreenings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,DateAndTime,TicketPrice,Id")] MovieScreening movieScreening)
        {
            if (ModelState.IsValid)
            {
                movieScreening.Id = Guid.NewGuid();
                _context.Add(movieScreening);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", movieScreening.MovieId);
            return View(movieScreening);
        }

        // GET: MovieScreenings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MovieScreenings == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.MovieScreenings.FindAsync(id);
            if (movieScreening == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieDescription", movieScreening.MovieId);
            return View(movieScreening);
        }

        // POST: MovieScreenings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MovieId,DateAndTime,TicketPrice,Id")] MovieScreening movieScreening)
        {
            if (id != movieScreening.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieScreening);
                    await _context.SaveChangesAsync();
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieDescription", movieScreening.MovieId);
            return View(movieScreening);
        }

        // GET: MovieScreenings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MovieScreenings == null)
            {
                return NotFound();
            }

            var movieScreening = await _context.MovieScreenings
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieScreening == null)
            {
                return NotFound();
            }

            return View(movieScreening);
        }

        // POST: MovieScreenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MovieScreenings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MovieScreenings'  is null.");
            }
            var movieScreening = await _context.MovieScreenings.FindAsync(id);
            if (movieScreening != null)
            {
                _context.MovieScreenings.Remove(movieScreening);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieScreeningExists(Guid id)
        {
          return (_context.MovieScreenings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
