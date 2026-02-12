using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class UserPurchaseRepository
    {
        private readonly PatchSellerDbContext _context;

        public UserPurchaseRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<UserPurchase>> GetAll()
        {
            try
            {
                return await _context.UserPurchases.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<UserPurchase>> GetPurchaseByUserId(int userId)
        {
            try
            {
                return await _context.UserPurchases.Where(x => x.UserId == userId).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserPurchase> GetById(int id)
        {
            try
            {
                return await _context.UserPurchases.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserPurchase> Create(UserPurchase entity)
        {
            try
            {
                var added = _context.UserPurchases.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserPurchase> Update(UserPurchase entity)
        {
            try
            {
                _context.UserPurchases.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
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
                var entity = await _context.UserPurchases.FindAsync(id);
                if (entity == null)
                    return false;

                _context.UserPurchases.Remove(entity);
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

