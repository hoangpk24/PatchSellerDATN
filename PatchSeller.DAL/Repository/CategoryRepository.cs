using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class CategoryRepository
    {
        private readonly PatchSellerDbContext _context;

        public CategoryRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                return await _context.Categories
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null && category.Delete == true)
                    return null;
                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> Create(Category category)
        {
            try
            {
                category.Delete = false;
                var added = _context.Categories.Add(category).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> Update(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return category;
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
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                    return false;

                category.Delete = true;
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

