using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();

        Order GetOrderDetails(Guid id);

        void PlaceOrder(Order entity);

        List<Order> GetAllOrdersByUser(string userId);
    }
}
