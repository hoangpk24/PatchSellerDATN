using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PatchImageRepository
    {
        private readonly PatchSellerDbContext _context;

        public PatchImageRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<PatchImage>> GetAll()
        {
            try
            {
                return await _context.PatchImages
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PatchImage> GetById(int id)
        {
            try
            {
                var image = await _context.PatchImages.FindAsync(id);
                if (image != null && image.Delete == true)
                    return null;
                return image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PatchImage> Create(PatchImage image)
        {
            try
            {
                image.Delete = false;
                var added = _context.PatchImages.Add(image).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PatchImage> Update(PatchImage image)
        {
            try
            {
                _context.PatchImages.Update(image);
                await _context.SaveChangesAsync();
                return image;
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
                var image = await _context.PatchImages.FindAsync(id);
                if (image == null)
                    return false;

                image.Delete = true;
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

