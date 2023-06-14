using CinemaTickets.Domain.DTOs;

namespace CinemaTickets.Service.Interface
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();

        OrderDto GetOrderById(Guid id);

        void PlaceNewOrder(string userId);

        List<OrderDto> GetAllOrdersByUser(string userId);
    }
}
