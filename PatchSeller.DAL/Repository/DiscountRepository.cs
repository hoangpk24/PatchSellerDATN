using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class DiscountRepository
    {
        private readonly PatchSellerDbContext _context;

        public DiscountRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Discount>> GetAll()
        {
            try
            {
                return await _context.Discounts
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> GetById(int id)
        {
            try
            {
                var discount = await _context.Discounts.FindAsync(id);
                if (discount != null && discount.Delete == true)
                    return null;
                return discount;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> Create(Discount discount)
        {
            try
            {
                discount.Delete = false;
                var added = _context.Discounts.Add(discount).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Discount> Update(Discount discount)
        {
            try
            {
                _context.Discounts.Update(discount);
                await _context.SaveChangesAsync();
                return discount;
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
                var discount = await _context.Discounts.FindAsync(id);
                if (discount == null)
                    return false;

                discount.Delete = true;
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

