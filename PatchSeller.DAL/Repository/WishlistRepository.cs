using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class WishlistRepository
    {
        private readonly PatchSellerDbContext _context;

        public WishlistRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Wishlist>> GetAll()
        {
            try
            {
                return await _context.Wishlists.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Wishlist> GetWishlistByCustomerAndGame(int customerId, int gameId)
        {
            try
            {
                var wishlist = await _context.Wishlists
                    .FirstOrDefaultAsync(x => x.GameId == gameId
                        && x.UserId == customerId);
                return wishlist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Wishlist>> GetAllByUserId(int userId)
        {
            try
            {
                return await _context.Wishlists
                    .Where(w => w.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Wishlist> GetById(int id)
        {
            try
            {
                return await _context.Wishlists.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Wishlist> Create(Wishlist wishlist)
        {
            try
            {
                var added = _context.Wishlists.Add(wishlist).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Wishlist> Update(Wishlist wishlist)
        {
            try
            {
                _context.Wishlists.Update(wishlist);
                await _context.SaveChangesAsync();
                return wishlist;
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
                var wishlist = await _context.Wishlists.FindAsync(id);
                if (wishlist == null)
                    return false;

                _context.Wishlists.Remove(wishlist);
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

