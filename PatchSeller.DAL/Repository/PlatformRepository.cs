using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PlatformRepository
    {
        private readonly PatchSellerDbContext _context;

        public PlatformRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Platform>> GetAll()
        {
            try
            {
                return await _context.Platforms
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Platform> GetById(int id)
        {
            try
            {
                var entity = await _context.Platforms.FindAsync(id);
                if (entity != null && entity.Delete == true)
                    return null;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Platform> Create(Platform entity)
        {
            try
            {
                entity.Delete = false;
                var added = _context.Platforms.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Platform> Update(Platform entity)
        {
            try
            {
                _context.Platforms.Update(entity);
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
                var entity = await _context.Platforms.FindAsync(id);
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

