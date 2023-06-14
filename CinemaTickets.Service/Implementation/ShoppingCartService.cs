using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTOs;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTickets.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMovieScreeningRepository _movieScreeningRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
            IUserRepository userRepository,
            IMovieScreeningRepository movieScreeningRepository,
            IRepository<TicketInShoppingCart> ticketInShoppingCartRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._userRepository = userRepository;
            this._movieScreeningRepository = movieScreeningRepository;
            this._ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var user = this._userRepository.Get(userId);
            var cart = user.ShoppingCart;
            var ticketPriceList = cart.TicketsInShoppingCart.Select(z => new { Price = z.MovieScreening.TicketPrice, Quantity = z.Quantity })
                .ToList();

            int totalPrice = 0;
            foreach(var item in ticketPriceList)
            {
                totalPrice += item.Quantity * item.Price;
            }

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Tickets = cart.TicketsInShoppingCart.ToList(),
                TotalPrice = totalPrice
            };

            return dto;
        }

        public void InsertItem(AddToShoppingCartDto dto, string userId)
        {
            var user = this._userRepository.Get(userId);
            var cart = user.ShoppingCart;

            if (dto.MovieScreeningId != null)
            {
                var screening = this._movieScreeningRepository.Get(dto.MovieScreeningId);

                if (screening != null)
                {
                    TicketInShoppingCart item = new TicketInShoppingCart
                    {
                        Quantity = dto.Quantity,
                        MovieScreeningId = dto.MovieScreeningId,
                        MovieScreening = screening,
                        ShoppingCartId = cart.Id,
                        ShoppingCart = cart
                    };

                    this._ticketInShoppingCartRepository.Insert(item);
                }
            }
        }

        public void RemoveItem(Guid itemId, string userId)
        {
            var user = this._userRepository.Get(userId);
            var cart = user.ShoppingCart;

            cart.TicketsInShoppingCart.Remove(cart.TicketsInShoppingCart.Where(item => item.Id.Equals(itemId)).FirstOrDefault());
            this._shoppingCartRepository.Update(cart);
        }
    }
}
