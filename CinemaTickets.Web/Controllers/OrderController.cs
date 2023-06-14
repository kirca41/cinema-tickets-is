using CinemaTickets.Service.Interface;
using ClosedXML.Excel;
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

        public IActionResult CreateInvoiceForOrdersWithGenre(string genre)
        {
            var orders = this._orderService.GetAllOrdersWithGenre(genre);

            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("All Orders");

                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Customer Email";

                for (int i = 1; i <= orders.Count(); i++)
                {
                    var item = orders[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Order.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Order.User.Email.ToString();

                    for (int p = 1; p <= item.Order.Tickets.Count(); p++)
                    {
                        var screening = item.Order.Tickets.ElementAt(p - 1).MovieScreening;
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Movie: " + screening.Movie.MovieName +
                                       ", played at: " + screening.DateAndTime.ToString() +
                                       ", with ticket price: " + screening.TicketPrice.ToString() +
                                       "$, and quantity: " + item.Order.Tickets.ElementAt(p - 1).Quantity.ToString());
                        worksheet.Cell(1, p + 4).Value = "Movie-" + (p);
                        worksheet.Cell(i + 1, p + 4).Value = sb.ToString();
                    }

                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
