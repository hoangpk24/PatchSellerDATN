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

        public async Task<List<Rank>> GetAll(string? keyword)
        {
            try
            {
                if (keyword != null) {
                    var lst1 = await _context.Ranks.Where(x => x.RankName.ToLower() == keyword.ToLower() && x.Delete != true).ToListAsync();

                    if(lst1.Any()) lst1.OrderBy(x => x.MiniumSpend).ToList();

                    return lst1;
                }

                var lst = await _context.Ranks
                    .Where(x => x.Delete != true)
                    .ToListAsync();

                if (lst.Any()) lst.OrderBy(x => x.MiniumSpend).ToList();

                return lst;
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
                if (rank == null || (rank != null && rank.Delete == true))
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
                var existingRank = await _context.Ranks
                    .FirstOrDefaultAsync(r => r.RankName.ToLower() == rank.RankName.ToLower() && r.Delete != true);
                if (existingRank != null)
                {
                    throw new InvalidOperationException("DUPLICATE_RANK_NAME");
                }

                var existingRankPoint = await _context.Ranks
                    .FirstOrDefaultAsync(r => r.MiniumSpend == rank.MiniumSpend && r.Delete != true);
                if (existingRankPoint != null)
                {
                    throw new InvalidOperationException("DUPLICATE_RANK_POINT");
                }

                rank.Delete = false;
                rank.CreatedAt = DateTime.Now;
                var added = _context.Ranks.Add(rank).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (InvalidOperationException)
            {
                throw;
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
                var existingRank = await _context.Ranks.FindAsync(rank.RankId);
                if (existingRank == null || (existingRank != null && existingRank.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateRankName = await _context.Ranks
                    .FirstOrDefaultAsync(r => r.RankName.ToLower() == rank.RankName.ToLower() && r.RankId != rank.RankId && r.Delete != true);

                if (duplicateRankName != null)
                {
                    throw new InvalidOperationException("DUPLICATE_RANK_NAME");
                }

                var duplicateRankPoint = await _context.Ranks
                    .FirstOrDefaultAsync(r => r.MiniumSpend == rank.MiniumSpend && r.RankId != rank.RankId && r.Delete != true);
                if (duplicateRankPoint != null)
                {
                    throw new InvalidOperationException("DUPLICATE_RANK_POINT");
                }

                existingRank.RankName = rank.RankName;
                existingRank.MiniumSpend = rank.MiniumSpend;
                existingRank.Description = rank.Description;
                existingRank.Status = rank.Status;
                existingRank.Delete = rank.Delete;

                _context.Ranks.Update(existingRank);
                await _context.SaveChangesAsync();
                return rank;
            }
            catch (InvalidOperationException)
            {
                throw;
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
                if (rank == null || (rank != null && rank.Delete == true))
                    throw new InvalidOperationException("NOT_FOUND");

                rank.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

