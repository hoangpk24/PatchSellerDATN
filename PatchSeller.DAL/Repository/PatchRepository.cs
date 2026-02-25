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

        private IQueryable<Patch> GetPublicQueryWithIncludes()
        {
            return _context.Patches
                .Include(p => p.Game)!.ThenInclude(g => g.Publisher)
                .Include(p => p.Game)!.ThenInclude(g => g.GameCategories)!.ThenInclude(gc => gc.Category)
                .Include(p => p.PatchImages)
                .Include(p => p.PatchVersions)
                .Where(p => p.Delete != true);
        }

        public async Task<List<Patch>> GetAllForPublic(string? keyword)
        {
            try
            {
                var query = await GetPublicQueryWithIncludes().ToListAsync();

                var filtered = query
                    .Where(p =>
                        p.Status == 1 &&
                        p.Game != null &&
                        p.Game.Delete != true &&
                        p.Game.Status == 1 &&
                        p.Game.Publisher != null &&
                        p.Game.Publisher.Delete != true &&
                        p.Game.Publisher.Status == 1 &&
                        p.Game.GameCategories != null &&
                        p.Game.GameCategories.Any(gc =>
                            gc.Delete != true &&
                            gc.Category != null &&
                            gc.Category.Delete != true &&
                            gc.Category.Status == 1));

                if (!string.IsNullOrEmpty(keyword))
                {
                    filtered = filtered.Where(x =>
                        x.Name.Contains(keyword) ||
                        (x.Description != null && x.Description.Contains(keyword)));
                }

                return filtered.ToList();
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

        public async Task<List<Patch>> GetByGameIdForPublic(int gameId)
        {
            try
            {
                var query = await GetPublicQueryWithIncludes()
                    .Where(p => p.GameId == gameId)
                    .ToListAsync();

                var filtered = query
                    .Where(p =>
                        p.Status == 1 &&
                        p.Game != null &&
                        p.Game.Delete != true &&
                        p.Game.Status == 1 &&
                        p.Game.Publisher != null &&
                        p.Game.Publisher.Delete != true &&
                        p.Game.Publisher.Status == 1 &&
                        p.Game.GameCategories != null &&
                        p.Game.GameCategories.Any(gc =>
                            gc.Delete != true &&
                            gc.Category != null &&
                            gc.Category.Delete != true &&
                            gc.Category.Status == 1));

                return filtered.ToList();
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
                    .Include(x => x.Game)
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

        public async Task<Patch> GetByIdForPublic(int id)
        {
            try
            {
                var patch = await GetPublicQueryWithIncludes()
                    .FirstOrDefaultAsync(p => p.PatchId == id);

                if (patch == null)
                {
                    return null;
                }

                var isValid =
                    patch.Status == 1 &&
                    patch.Game != null &&
                    patch.Game.Delete != true &&
                    patch.Game.Status == 1 &&
                    patch.Game.Publisher != null &&
                    patch.Game.Publisher.Delete != true &&
                    patch.Game.Publisher.Status == 1 &&
                    patch.Game.GameCategories != null &&
                    patch.Game.GameCategories.Any(gc =>
                        gc.Delete != true &&
                        gc.Category != null &&
                        gc.Category.Delete != true &&
                        gc.Category.Status == 1);

                return isValid ? patch : null;
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

