using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class DownloadLogRepository
    {
        private readonly PatchSellerDbContext _context;

        public DownloadLogRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<DownloadLog>> GetAll()
        {
            try
            {
                return await _context.DownloadLogs.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DownloadLog> GetById(int id)
        {
            try
            {
                return await _context.DownloadLogs.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DownloadLog> Create(DownloadLog entity)
        {
            try
            {
                var added = _context.DownloadLogs.Add(entity).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DownloadLog> Update(DownloadLog entity)
        {
            try
            {
                _context.DownloadLogs.Update(entity);
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
                var entity = await _context.DownloadLogs.FindAsync(id);
                if (entity == null)
                    return false;

                _context.DownloadLogs.Remove(entity);
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

