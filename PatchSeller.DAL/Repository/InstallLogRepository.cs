using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class InstallLogRepository
    {
        private readonly PatchSellerDbContext _context;

        public InstallLogRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<InstallLog>> GetAll()
        {
            try
            {
                return await _context.Set<InstallLog>().ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InstallLog> GetById(int id)
        {
            try
            {
                return await _context.Set<InstallLog>().FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<InstallLog>> GetByPatchId(int patchId)
        {
            try
            {
                return await _context.Set<InstallLog>()
                    .Where(x => x.PatchId == patchId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<InstallLog>> GetByUserId(int userId)
        {
            try
            {
                return await _context.Set<InstallLog>()
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InstallLog> GetByUserAndPatchId(int userId, int patchId)
        {
            try
            {
                return await _context.Set<InstallLog>()
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.PatchId == patchId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InstallLog> GetByUserPatchBios(int userId, int patchId, string biosSerialNumber)
        {
            try
            {
                return await _context.Set<InstallLog>()
                    .FirstOrDefaultAsync(x =>
                        x.UserId == userId &&
                        x.PatchId == patchId &&
                        x.BIOSSerialNumber == biosSerialNumber);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InstallLog> Create(InstallLog entity)
        {
            try
            {
                var added = _context.Set<InstallLog>().Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InstallLog> Update(InstallLog entity)
        {
            try
            {
                _context.Set<InstallLog>().Update(entity);
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
                var entity = await _context.Set<InstallLog>().FindAsync(id);
                if (entity == null)
                    return false;

                _context.Set<InstallLog>().Remove(entity);
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

