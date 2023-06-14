using CinemaTickets.Service.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

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
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
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

        public IActionResult CreateInvoiceForOrder(Guid orderId)
        {
            var order = this._orderService.GetOrderById(orderId);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "SingleOrderInvoiceTemplate.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderId}}", order.Order.Id.ToString());
            document.Content.Replace("{{User}}", order.Order.User.Email.ToString());

            StringBuilder movies = new StringBuilder();
            int total = 0;

            foreach(var item in order.Order.Tickets)
            {
                total += item.Quantity * item.MovieScreening.TicketPrice;
                movies.AppendLine("Movie: " + item.MovieScreening.Movie.MovieName +
                                       ", played at: " + item.MovieScreening.DateAndTime.ToString() +
                                       ", with ticket price: " + item.MovieScreening.TicketPrice.ToString() +
                                       "$, and quantity: " + item.Quantity.ToString());
            }

            document.Content.Replace("{{MovieTickets}}", movies.ToString());
            document.Content.Replace("{{OrderedOn}}", order.Order.Timestamp.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "$");

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportOrderInvoice.pdf");
        }
    }
}
