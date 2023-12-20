using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Data;
using Cuchillos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.DataLayer.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementQuantity(ShoppingCart cartInDb, int quantity)
        {
            cartInDb.Quantity -= quantity;
            return cartInDb.Quantity;
        }

        public int IncrementQuantity(ShoppingCart cartInDb, int quantity)
        {
            cartInDb.Quantity += quantity;
            return cartInDb.Quantity;

        }
    }
}
