using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class ActionLogRepository
    {
        private readonly PatchSellerDbContext _context;

        public ActionLogRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<ActionLog>> GetAll()
        {
            try
            {
                return await _context.ActionLogs.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActionLog> GetById(int id)
        {
            try
            {
                return await _context.ActionLogs.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActionLog> Create(ActionLog log)
        {
            try
            {
                var added = _context.ActionLogs.Add(log).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActionLog> Update(ActionLog log)
        {
            try
            {
                _context.ActionLogs.Update(log);
                await _context.SaveChangesAsync();
                return log;
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
                var log = await _context.ActionLogs.FindAsync(id);
                if (log == null)
                    return false;

                _context.ActionLogs.Remove(log);
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

