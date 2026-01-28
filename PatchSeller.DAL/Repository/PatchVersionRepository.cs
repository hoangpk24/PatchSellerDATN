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

        public async Task<List<PatchVersion>> GetAll()
        {
            try
            {
                return await _context.PatchVersions
                    .Where(x => x.Delete != true)
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
                var version = await _context.PatchVersions.FindAsync(id);
                if (version != null && version.Delete == true)
                    return null;
                return version;
            }
            catch (Exception)
            {
                return null;
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

