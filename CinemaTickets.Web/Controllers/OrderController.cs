using CinemaTickets.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaTickets.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderController(IOrderService orderService,
            IShoppingCartService shoppingCartService)
        {
            this._orderService = orderService;
            this._shoppingCartService = shoppingCartService;
        }

        // Make access only for admin
        public IActionResult Index()
        {
            return View(this._orderService.GetAllOrders());
        }

        public IActionResult OrdersByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._orderService.GetAllOrdersByUser(userId));
        }

        public IActionResult Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.GetShoppingCartInfo(userId));
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            this._orderService.PlaceNewOrder(userId);

            return RedirectToAction("OrdersByUser");
        }
    }
}
