using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class RankRepository
    {
        private readonly PatchSellerDbContext _context;

        public RankRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Rank>> GetAll()
        {
            try
            {
                return await _context.Ranks
                    .Where(x => x.Delete != true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Rank> GetById(int id)
        {
            try
            {
                var rank = await _context.Ranks.FindAsync(id);
                if (rank != null && rank.Delete == true)
                    return null;
                return rank;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Rank> Create(Rank rank)
        {
            try
            {
                rank.Delete = false;
                var added = _context.Ranks.Add(rank).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Rank> Update(Rank rank)
        {
            try
            {
                _context.Ranks.Update(rank);
                await _context.SaveChangesAsync();
                return rank;
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
                var rank = await _context.Ranks.FindAsync(id);
                if (rank == null)
                    return false;

                rank.Delete = true;
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

