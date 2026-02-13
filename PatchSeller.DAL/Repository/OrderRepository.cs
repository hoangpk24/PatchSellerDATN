using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class OrderRepository
    {
        private readonly PatchSellerDbContext _context;

        public OrderRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Order>> GetAll()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Order>> GetAllByUserId(int userId)
        {
            try
            {
                return await _context.Orders.Where(x=>x.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> GetById(int id)
        {
            try
            {
                return await _context.Orders.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> GetByIdWithDetails(int id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Patch)
                            .ThenInclude(p => p!.Game)
                                .ThenInclude(g => g!.GameImages)
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Patch)
                            .ThenInclude(p => p!.PatchImages)
                    .Include(o => o.Discount)
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> Create(Order order)
        {
            try
            {
                var added = _context.Orders.Add(order).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Order> Update(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return order;
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
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                    return false;

                _context.Orders.Remove(order);
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

