using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTOs;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;

namespace CinemaTickets.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketInOrder> _ticketsInOrderRepository;

        public OrderService(IOrderRepository orderRepository,
            IUserRepository _userRepository,
            IRepository<TicketInOrder> ticketsInOrderRepository)
        {
            this._orderRepository = orderRepository;
            this._userRepository = _userRepository;
            this._ticketsInOrderRepository = ticketsInOrderRepository;
        }

        private int getTotalTicketPrice(ICollection<TicketInOrder> Tickets)
        {
            int totalPrice = 0;
            foreach(var item in Tickets)
            {
                totalPrice += item.Quantity * item.MovieScreening.TicketPrice;
            }

            return totalPrice;
        }

        public List<OrderDto> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders()
                .Select(z => new OrderDto
                {
                    Order = z,
                    TotalPrice = this.getTotalTicketPrice(z.Tickets)
                }).ToList();

        }

        public List<OrderDto> GetAllOrdersByUser(string userId)
        {
            return this._orderRepository.GetAllOrdersByUser(userId)
                .Select(z => new OrderDto
                {
                    Order = z,
                    TotalPrice = this.getTotalTicketPrice(z.Tickets)
                }).ToList(); ;
        }

        public OrderDto GetOrderById(Guid id)
        {
            var order =  this._orderRepository.GetOrderDetails(id);

            return new OrderDto
            {
                Order = order,
                TotalPrice = this.getTotalTicketPrice(order.Tickets)
            };
        }

        public void PlaceNewOrder(string userId)
        {
            var user = this._userRepository.Get(userId);

            Order order = new Order
            {
                UserId = userId,
                User = user,
                Timestamp = DateTime.Now
            };

            this._orderRepository.PlaceOrder(order);

            List<TicketInOrder> tickets = user.ShoppingCart.TicketsInShoppingCart
                .Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    MovieScreening = z.MovieScreening,
                    MovieScreeningId = z.MovieScreeningId,
                    Order = order,
                    OrderId = order.Id,
                    Quantity = z.Quantity
                }).ToList();

            foreach(var ticket in tickets)
            {
                this._ticketsInOrderRepository.Insert(ticket);
            }

            user.ShoppingCart.TicketsInShoppingCart.Clear();
            this._userRepository.Update(user);
        }
    }
}
