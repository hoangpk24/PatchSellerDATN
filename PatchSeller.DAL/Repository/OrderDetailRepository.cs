using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class OrderDetailRepository
    {
        private readonly PatchSellerDbContext _context;

        public OrderDetailRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            try
            {
                return await _context.OrderDetails.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderDetail> GetById(int id)
        {
            try
            {
                return await _context.OrderDetails.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderDetail> Create(OrderDetail detail)
        {
            try
            {
                var added = _context.OrderDetails.Add(detail).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderDetail> Update(OrderDetail detail)
        {
            try
            {
                _context.OrderDetails.Update(detail);
                await _context.SaveChangesAsync();
                return detail;
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
                var detail = await _context.OrderDetails.FindAsync(id);
                if (detail == null)
                    return false;

                _context.OrderDetails.Remove(detail);
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

