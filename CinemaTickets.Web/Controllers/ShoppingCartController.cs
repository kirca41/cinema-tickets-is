using CinemaTickets.Domain.DTOs;
using CinemaTickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaTickets.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMovieScreeningService _movieScreeningService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
             IMovieScreeningService movieScreeningService)
        {
            this._shoppingCartService = shoppingCartService;
            this._movieScreeningService = movieScreeningService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.GetShoppingCartInfo(userId));
        }

        public IActionResult AddItemToCart(Guid screeningId)
        {
            var screening = this._movieScreeningService.GetMovieScreeningById(screeningId);

            AddToShoppingCartDto dto = new AddToShoppingCartDto
            {
                MovieScreeningId = screeningId,
                MovieScreening = screening,
                Quantity = 0
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult AddItemToCart(AddToShoppingCartDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            this._shoppingCartService.InsertItem(dto, userId);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemFromCart(Guid itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            this._shoppingCartService.RemoveItem(itemId, userId);

            return RedirectToAction("Index");
        }
    }
}
