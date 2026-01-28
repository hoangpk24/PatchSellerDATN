using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class CartRepository
    {
        private readonly PatchSellerDbContext _context;

        public CartRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Cart>> GetAll()
        {
            try
            {
                return await _context.Carts
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> GetById(int id)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart != null && cart.Delete == true)
                    return null;
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> Create(Cart cart)
        {
            try
            {
                cart.Delete = false;
                var addedCart = _context.Carts.Add(cart).Entity;
                await _context.SaveChangesAsync();
                return addedCart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> Update(Cart cart)
        {
            try
            {
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart == null)
                    return false;

                cart.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await GetAll();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await GetById(id);
        }

        public async Task<Cart> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _context.Carts
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Delete != true);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Cart> AddCart(Cart cart)
        {
            return await Create(cart);
        }
    }
}

