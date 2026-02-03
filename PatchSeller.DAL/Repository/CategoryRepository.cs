using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatchSeller.DAL.Repository
{
    public class CategoryRepository
    {
        private readonly PatchSellerDbContext _context;

        public CategoryRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Category>> GetAll(string? keyword)
        {
            try
            {
                if (keyword == null) { 
                    return await _context.Categories
                        .Where(x => x.Delete != true)
                        .ToListAsync();
                }
                else
                {
                    List<Category> listCategory = new List<Category>();
                    listCategory = _context.Categories.Where(x => x.Delete != true && x.CategoryName.Contains(keyword)).ToList();
                    return listCategory;
                }
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
                if (category == null || (category != null && category.Delete == true))
                {
                    return null;
                }
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
                var duplicateName = await _context.Categories.AnyAsync(c => c.CategoryName.ToLower() == category.CategoryName.ToLower() && c.Delete != true);

                if(duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                category.Delete = false;
                category.CreatedAt = DateTime.Now;
                var added = _context.Categories.Add(category).Entity;
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

        public async Task<Category> Update(Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(category.CategoryId);

                if(existingCategory == null || (existingCategory != null && existingCategory.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateName = await _context.Categories.AnyAsync(c => c.CategoryId != category.CategoryId && c.CategoryName == category.CategoryName && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Description = category.Description;
                existingCategory.Status = category.Status;
                existingCategory.Delete = category.Delete;

                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
                return category;
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
                var category = await _context.Categories.FindAsync(id);

                if (category == null || (category != null && category.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                category.Delete = true;

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
    }
}

