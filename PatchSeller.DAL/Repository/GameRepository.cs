using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class GameRepository
    {
        private readonly PatchSellerDbContext _context;

        public GameRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Game>> GetAll(string? keyword)
        {
            try
            {
                if(keyword == null)
                {
                    return await _context.Games
                 .Where(x => x.Delete != true)
                 .ToListAsync();
                }
                else
                {
                 var lstGame = await _context.Games
                .Where(x => x.Delete != true)
                .ToListAsync();
                    return lstGame.Where(x=>x.Title.Contains(keyword) || x.Description.Contains(keyword)).ToList();
                }
             
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Game>> GetAllDetail(string? keyword)
        {
            try
            {
                var query = _context.Games
                    .Where(x => x.Delete != true)
                    .Include(g => g.Publisher)
                    .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                    .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                    .Include(g => g.Patches)
                    .Include(g => g.GameImages).ToList();

                if (keyword != null)
                {
                    query = query.Where(x => x.Title.Contains(keyword) || (x.Description != null && x.Description.Contains(keyword))).ToList();
                }

                return  query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Game> GetById(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);
                if (game != null && game.Delete == true)
                    return null;
                return game;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Game> GetByIdDetail(int id)
        {
            try
            {
                var game = await _context.Games
                    .Where(x => x.GameId == id && x.Delete != true)
                    .Include(g => g.Publisher)
                    .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                    .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                    .Include(g => g.Patches)
                    .Include(g => g.GameImages)
                    .FirstOrDefaultAsync();
                return game;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Game> Create(Game game)
        {
            try
            {
                game.Delete = false;
                var added = _context.Games.Add(game).Entity;
                await _context.SaveChangesAsync();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Game> Update(Game game)
        {
            try
            {
                _context.Games.Update(game);
                await _context.SaveChangesAsync();
                return game;
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
                var game = await _context.Games.FindAsync(id);
                if (game == null)
                    return false;

                game.Delete = true;
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

