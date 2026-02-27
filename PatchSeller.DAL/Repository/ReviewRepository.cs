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

        public async Task<List<Review>> GetAllDetail(string? keyword)
        {
            try
            {
                var query = _context.Reviews
                            .Include(x => x.User)
                            .Include(x => x.Patch)
                            .AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var k = keyword.ToLower(); 

                    query = query.Where(x =>
                        x.Patch.Name.ToLower().Contains(k) ||
                        x.User.FullName.ToLower().Contains(k) ||
                        x.UserName.ToLower().Contains(k) ||
                        x.User.Email.ToLower().Contains(k));
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Review>();
            }
        }

        public async Task<List<Review>> GetAllByPatchId(int patchId)
        {
            try
            {
                var result = await _context.Reviews.Where(x => x.PatchId == patchId).ToListAsync();

                if(result == null)
                {
                    return new List<Review>();
                }

                return result;
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
                var obj = await _context.Reviews.FindAsync(id);
                if(obj == null || (obj != null && obj.Status != 1))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                return obj;
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

        public async Task<Review> Create(Review review)
        {
            try
            {
                review.Status = 1;
                review.CreatedAt = DateTime.Now;
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
                var obj = await _context.Reviews.FindAsync(review.ReviewId);
                if (obj == null)
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                obj.Title = review.Title;
                obj.Content = review.Content;
                obj.Overall = review.Overall;
                obj.PatchId = review.PatchId;
                obj.ReviewId = review.ReviewId;
                obj.UserId = review.UserId;
                obj.UserName = review.UserName;
                obj.Status = review.Status;

                _context.Reviews.Update(obj);
                await _context.SaveChangesAsync();
                return review;
            }
            catch(InvalidOperationException)
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
                var obj = await _context.Reviews.FindAsync(id);
                if (obj == null || (obj != null && obj.Status != 1))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                _context.Reviews.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

