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

        public async Task<List<Game>> GetAll()
        {
            try
            {
                return await _context.Games
                    .Where(x => x.Delete != true)
                    .ToListAsync();
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

