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

        public async Task<List<Platform>> GetAll(string? keyword)
        {
            try
            {
                if(keyword != null)
                {
                    return await _context.Platforms
                        .Where(x => x.Delete != true && x.Name.ToLower().Contains(keyword.ToLower()))
                        .ToListAsync();
                }

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
                var duplicateName = await _context.Platforms.AnyAsync(c => c.Name.ToLower() == entity.Name.ToLower() && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                entity.Delete = false;
                entity.CreatedAt = DateTime.Now;
                var added = _context.Platforms.Add(entity).Entity;
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

        public async Task<Platform> Update(Platform entity)
        {
            try
            {
                var existingPlatform = await _context.Platforms.FindAsync(entity.PlatformId);

                if (existingPlatform == null || (existingPlatform != null && existingPlatform.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateName = await _context.Platforms.AnyAsync(c => c.PlatformId != entity.PlatformId && c.Name.ToLower() == entity.Name.ToLower() && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                existingPlatform.Name = entity.Name;
                existingPlatform.Description = entity.Description;
                existingPlatform.Status = entity.Status;
                existingPlatform.Delete = entity.Delete;

                _context.Platforms.Update(existingPlatform);
                await _context.SaveChangesAsync();
                return entity;
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
                var entity = await _context.Platforms.FindAsync(id);

                if (entity == null || (entity != null && entity.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                entity.Delete = true;

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

