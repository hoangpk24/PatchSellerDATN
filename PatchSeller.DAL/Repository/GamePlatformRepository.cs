using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class GamePlatformRepository
    {
        private readonly PatchSellerDbContext _context;

        public GamePlatformRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<GamePlatform>> GetAll()
        {
            try
            {
                return await _context.GamePlatforms
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GamePlatform> GetById(int id)
        {
            try
            {
                var entity = await _context.GamePlatforms.FindAsync(id);
                if (entity != null && entity.Delete == true)
                    return null;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<GamePlatform>> GetByGameId(int gameId, bool isDelete = true)
        {
            try
            {
                if(isDelete)
                {
                    return await _context.GamePlatforms
                        .Where(x => x.Delete != true && x.GameId == gameId)
                        .ToListAsync();
                }
                return await _context.GamePlatforms
                    .Where(x => x.GameId == gameId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<GamePlatform> Create(GamePlatform entity)
        {
            try
            {
                entity.Delete = false;
                var added = _context.GamePlatforms.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GamePlatform> Update(GamePlatform entity)
        {
            try
            {
                _context.GamePlatforms.Update(entity);
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
                var entity = await _context.GamePlatforms.FindAsync(id);
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

