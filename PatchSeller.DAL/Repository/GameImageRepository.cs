using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class GameImageRepository
    {
        private readonly PatchSellerDbContext _context;

        public GameImageRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<GameImage>> GetAll()
        {
            try
            {
                return await _context.GameImages
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GameImage> GetById(int id)
        {
            try
            {
                var entity = await _context.GameImages.FindAsync(id);
                if (entity != null && entity.Delete == true)
                    return null;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GameImage> Create(GameImage entity)
        {
            try
            {
                entity.Delete = false;
                var added = _context.GameImages.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GameImage> Update(GameImage entity)
        {
            try
            {
                _context.GameImages.Update(entity);
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
                var entity = await _context.GameImages.FindAsync(id);
                if (entity == null)
                    return false;

                entity.Delete = true;
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

