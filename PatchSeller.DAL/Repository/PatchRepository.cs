using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PatchRepository
    {
        private readonly PatchSellerDbContext _context;

        public PatchRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Patch>> GetAll(string? keyword)
        {
            try
            {
                var query = _context.Patches.Where(x => x.Delete != true);
                if (keyword != null)
                {
                    query = query.Where(x => x.Name.Contains(keyword) || (x.Description != null && x.Description.Contains(keyword)));
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Patch>> GetAllDetail(string? keyword)
        {
            try
            {
                var query = _context.Patches
                    .Where(x => x.Delete != true)
                    .Include(p => p.PatchImages)
                    .Include(p => p.Game)
                    .Include(p => p.PatchVersions).ToList();
                if (keyword != null)
                {
                    query = query.Where(x => x.Name.Contains(keyword) || (x.Description != null && x.Description.Contains(keyword))).ToList();
                }
                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Patch>> GetByGameId(int gameId)
        {
            try
            {
                return await _context.Patches
                    .Where(x => x.GameId == gameId && x.Delete != true)
                    .Include(p => p.PatchImages)
                    .Include(p => p.PatchVersions)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Patch> GetDetailById(int id)
        {
            try
            {
                var patch = await _context.Patches
                    .Where(x => x.PatchId == id && x.Delete != true)
                    .Include(x => x.PatchVersions)
                    .Include(x => x.UserPurchases)
                    .Include(x => x.PatchImages)
                    .FirstOrDefaultAsync();

                if (patch == null)
                {
                    return null;
                }
             
                return patch;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Patch> GetById(int id)
        {
            try
            {
                var patch = await _context.Patches.FindAsync(id);
                if (patch != null && patch.Delete == true)
                    return null;
                return patch;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Patch> Create(Patch patch)
        {
            try
            {
                patch.Delete = false;
                patch.CreatedAt = DateTime.Now;
                var added = _context.Patches.Add(patch).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Patch> Update(Patch patch)
        {
            try
            {
                _context.Patches.Update(patch);
                await _context.SaveChangesAsync();
                return patch;
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
                var patch = await _context.Patches.FindAsync(id);
                if (patch == null)
                    return false;

                patch.Delete = true;
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

