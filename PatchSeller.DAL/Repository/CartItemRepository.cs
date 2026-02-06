using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class CartItemRepository
    {
        private readonly PatchSellerDbContext _context;

        public CartItemRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<CartItem>> GetAll()
        {
            try
            {
                return await _context.CartItems
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CartItem>> GetAllByUserId(int userId)
        {
            try
            {
                return await _context.CartItems
                    .Where(x => x.Delete != true && x.Cart != null && x.Cart.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> GetById(int id)
        {
            try
            {
                var item = await _context.CartItems.FindAsync(id);
                if (item != null && item.Delete == true)
                    return null;
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> Create(CartItem item)
        {
            try
            {
                item.Delete = false;
                var added = _context.CartItems.Add(item).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CartItem>> GetByUserIdWithDetails(int userId)
        {
            try
            {
                return await _context.CartItems
                    .Where(ci => ci.Delete != true
                                 && ci.Cart != null
                                 && ci.Cart.UserId == userId)
                    .Include(ci => ci.Patch)
                        .ThenInclude(p => p.Game)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CartItem> Update(CartItem item)
        {
            try
            {
                _context.CartItems.Update(item);
                await _context.SaveChangesAsync();
                return item;
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
                var item = await _context.CartItems.FindAsync(id);
                if (item == null)
                    return false;

                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

