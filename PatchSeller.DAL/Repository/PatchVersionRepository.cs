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

