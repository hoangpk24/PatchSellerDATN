using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class PatchVersionRepository
    {
        private readonly PatchSellerDbContext _context;

        public PatchVersionRepository()
        {
            _context = new PatchSellerDbContext();
        }

        private IQueryable<PatchVersion> GetQueryWithIncludes()
        {
            return _context.PatchVersions
                .Include(pv => pv.Patch)
                    .ThenInclude(p => p!.Game)
                .Include(pv => pv.Staff)
                .Include(pv => pv.PatchImages);
        }

        private IQueryable<PatchVersion> GetPublicQueryWithIncludes()
        {
            return _context.PatchVersions
                .Include(pv => pv.Patch)!.ThenInclude(p => p.Game)!.ThenInclude(g => g.Publisher)
                .Include(pv => pv.Patch)!.ThenInclude(p => p.Game)!.ThenInclude(g => g.GameCategories)!.ThenInclude(gc => gc.Category)
                .Include(pv => pv.Staff)
                .Include(pv => pv.PatchImages)
                .Where(pv => pv.Delete != true);
        }

        public async Task<List<PatchVersion>> GetAll()
        {
            try
            {
                return await GetQueryWithIncludes()
                    .Where(x => x.Delete != true)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PatchVersion>> GetAllForPublic()
        {
            try
            {
                var list = await GetPublicQueryWithIncludes()
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();

                var filtered = list.Where(pv =>
                    pv.Status == 1 &&
                    pv.Patch != null &&
                    pv.Patch.Delete != true &&
                    pv.Patch.Status == 1 &&
                    pv.Patch.Game != null &&
                    pv.Patch.Game.Delete != true &&
                    pv.Patch.Game.Status == 1 &&
                    pv.Patch.Game.Publisher != null &&
                    pv.Patch.Game.Publisher.Delete != true &&
                    pv.Patch.Game.Publisher.Status == 1 &&
                    pv.Patch.Game.GameCategories != null &&
                    pv.Patch.Game.GameCategories.Any(gc =>
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

        public async Task<PatchVersion> GetById(int id)
        {
            try
            {
                var version = await GetQueryWithIncludes()
                    .FirstOrDefaultAsync(x => x.PatchVersionId == id && x.Delete != true);
                return version;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PatchVersion> GetByIdForPublic(int id)
        {
            try
            {
                var version = await GetPublicQueryWithIncludes()
                    .FirstOrDefaultAsync(x => x.PatchVersionId == id);

                if (version == null)
                {
                    return null;
                }

                var isValid =
                    version.Status == 1 &&
                    version.Patch != null &&
                    version.Patch.Delete != true &&
                    version.Patch.Status == 1 &&
                    version.Patch.Game != null &&
                    version.Patch.Game.Delete != true &&
                    version.Patch.Game.Status == 1 &&
                    version.Patch.Game.Publisher != null &&
                    version.Patch.Game.Publisher.Delete != true &&
                    version.Patch.Game.Publisher.Status == 1 &&
                    version.Patch.Game.GameCategories != null &&
                    version.Patch.Game.GameCategories.Any(gc =>
                        gc.Delete != true &&
                        gc.Category != null &&
                        gc.Category.Delete != true &&
                        gc.Category.Status == 1);

                return isValid ? version : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PatchVersion>> GetByPatchId(int patchId)
        {
            try
            {
                return await GetQueryWithIncludes()
                    .Where(x => x.PatchId == patchId && x.Delete != true)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PatchVersion>> GetByPatchIdForPublic(int patchId)
        {
            try
            {
                var list = await GetPublicQueryWithIncludes()
                    .Where(x => x.PatchId == patchId)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();

                var filtered = list.Where(pv =>
                    pv.Status == 1 &&
                    pv.Patch != null &&
                    pv.Patch.Delete != true &&
                    pv.Patch.Status == 1 &&
                    pv.Patch.Game != null &&
                    pv.Patch.Game.Delete != true &&
                    pv.Patch.Game.Status == 1 &&
                    pv.Patch.Game.Publisher != null &&
                    pv.Patch.Game.Publisher.Delete != true &&
                    pv.Patch.Game.Publisher.Status == 1 &&
                    pv.Patch.Game.GameCategories != null &&
                    pv.Patch.Game.GameCategories.Any(gc =>
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

        public async Task<List<PatchVersion>> GetByGameId(int gameId)
        {
            try
            {
                return await GetQueryWithIncludes()
                    .Where(x => x.Patch != null && x.Patch.GameId == gameId && x.Delete != true)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PatchVersion>> GetByGameIdForPublic(int gameId)
        {
            try
            {
                var list = await GetPublicQueryWithIncludes()
                    .Where(x => x.Patch != null && x.Patch.GameId == gameId)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();

                var filtered = list.Where(pv =>
                    pv.Status == 1 &&
                    pv.Patch != null &&
                    pv.Patch.Delete != true &&
                    pv.Patch.Status == 1 &&
                    pv.Patch.Game != null &&
                    pv.Patch.Game.Delete != true &&
                    pv.Patch.Game.Status == 1 &&
                    pv.Patch.Game.Publisher != null &&
                    pv.Patch.Game.Publisher.Delete != true &&
                    pv.Patch.Game.Publisher.Status == 1 &&
                    pv.Patch.Game.GameCategories != null &&
                    pv.Patch.Game.GameCategories.Any(gc =>
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

        public async Task<bool> PatchExists(int patchId)
        {
            try
            {
                return await _context.Patches.AnyAsync(p => p.PatchId == patchId && p.Delete != true);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> VersionNameExists(int patchId, string versionName, int? excludePatchVersionId = null)
        {
            try
            {
                var query = _context.PatchVersions
                    .Where(x => x.PatchId == patchId && x.VersionName == versionName && x.Delete != true);
                if (excludePatchVersionId.HasValue)
                {
                    query = query.Where(x => x.PatchVersionId != excludePatchVersionId.Value);
                }
                return await query.AnyAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PatchVersion> Create(PatchVersion version)
        {
            try
            {
                version.Delete = false;
                version.CreateAt = DateTime.Now;
                var added = _context.PatchVersions.Add(version).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PatchVersion> Update(PatchVersion version)
        {
            try
            {
                _context.PatchVersions.Update(version);
                await _context.SaveChangesAsync();
                return version;
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
                var version = await _context.PatchVersions.FindAsync(id);
                if (version == null)
                    return false;

                version.Delete = true;
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

