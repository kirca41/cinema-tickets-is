using CinemaTickets.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Interface
{
    public interface IShoppingCartService
    {
        void InsertItem(AddToShoppingCartDto dto, string userId);

        void RemoveItem(Guid itemId, string userId);

        ShoppingCartDto GetShoppingCartInfo(string userId);
    }
}
