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

        public async Task<int> GetUserTimeUsed(string code, int userId)
        {
            try
            {
                var discountCode = await GetDiscountCodeByCode(code);
                return await _context.Orders.Where(x => x.UserId == userId && x.DiscountId == discountCode.DiscountId && x.Status!=3).CountAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<Discount> GetDiscountCodeByCode(string code)
        {
            try
            {
                var discount = await _context.Discounts.FirstOrDefaultAsync(x=>x.Code == code);
                if (discount == null || (discount != null && discount.Delete == true))
                    return null;
                return discount;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<List<Discount>> GetAll(
            string? keyword = null, 
            string? discountType = null,
            int? rankId = 0,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                var query = _context.Discounts
                            .Where(x => x.Delete != true)
                            .AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(x => x.Code.Contains(keyword));
                }

                if (!string.IsNullOrEmpty(discountType))
                {
                    query = query.Where(x => x.DiscountType == discountType);
                }

                if (rankId != null && rankId > 0)
                {
                    query = query.Where(x => x.RankId == rankId);
                }

                if (startDate.HasValue)
                {
                    query = query.Where(x => x.StartDate.Date >= startDate.Value.Date);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(x => x.EndDate.Date <= endDate.Value.Date);
                }

                var discountCodes = await query.ToListAsync();
                return discountCodes;
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
                if (discount == null || (discount != null && discount.Delete == true))
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
                var existingDiscount = await _context.Discounts
                    .FirstOrDefaultAsync(d => d.Code.ToLower() == discount.Code.ToLower() && d.Delete != true);

                if (existingDiscount != null)
                {
                    throw new InvalidOperationException("DUPLICATE_CODE");
                }
                discount.Delete = false;
                discount.CreatedAt = DateTime.Now;
                if(discount.RankId == null || discount.RankId < 1)
                {
                    discount.RankId = null;
                }
                var added = _context.Discounts.Add(discount).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (InvalidOperationException)
            {
                throw;
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
                var existingDiscount = await _context.Discounts.FindAsync(discount.DiscountId);

                if (existingDiscount == null || (existingDiscount != null && existingDiscount.Delete == true))
                    throw new InvalidOperationException("NOT_FOUND");

                var duplicateDiscount = await _context.Discounts.AnyAsync(d => d.Code.ToLower() == discount.Code.ToLower() && d.Delete == false && d.DiscountId != discount.DiscountId);
                
                if(duplicateDiscount) {
                    throw new InvalidOperationException("DUPLICATE_CODE");
                }

                existingDiscount.Code = discount.Code;
                existingDiscount.DiscountType = discount.DiscountType;
                existingDiscount.Value = discount.Value;
                existingDiscount.MaxDiscount = discount.MaxDiscount;
                existingDiscount.MinOrderValue = discount.MinOrderValue;
                existingDiscount.UsageLimit = discount.UsageLimit;
                existingDiscount.LimitPerUser = discount.LimitPerUser;
                existingDiscount.UsedCount = discount.UsedCount;
                existingDiscount.StartDate = discount.StartDate;
                existingDiscount.EndDate = discount.EndDate;
                existingDiscount.Status = discount.Status;
                existingDiscount.Delete = discount.Delete;
                existingDiscount.RankId = discount.RankId != null && discount.RankId > 0 ? discount.RankId : null;

                _context.Discounts.Update(existingDiscount);
                await _context.SaveChangesAsync();
                return discount;
            }
            catch (InvalidOperationException)
            {
                throw;
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
                if (discount == null || (discount != null && discount.Delete == true))
                    throw new InvalidOperationException("NOT_FOUND");

                discount.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Discount>> GetAllListDiscount()
        {
            try
            {
                var query = _context.Discounts
                            .Where(x => x.Delete != true)
                            .AsQueryable();
                query = query.Where(x => x.StartDate.Date >= DateTime.Now.AddMinutes(10));
                var discountCodes = await query.ToListAsync();
                return discountCodes;
            }
            catch (Exception)
            {
                return null;
            }


        }
    }
}

