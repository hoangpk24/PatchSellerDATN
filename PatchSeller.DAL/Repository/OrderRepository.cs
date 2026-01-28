using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
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
                // Order không có trường Delete nên lấy tất cả
                return await _context.Orders.ToListAsync();
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

