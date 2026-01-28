using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class ReviewRepository
    {
        private readonly PatchSellerDbContext _context;

        public ReviewRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Review>> GetAll()
        {
            try
            {
                return await _context.Reviews.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> GetById(int id)
        {
            try
            {
                return await _context.Reviews.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> Create(Review review)
        {
            try
            {
                var added = _context.Reviews.Add(review).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Review> Update(Review review)
        {
            try
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
                return review;
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
                var review = await _context.Reviews.FindAsync(id);
                if (review == null)
                    return false;

                _context.Reviews.Remove(review);
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

