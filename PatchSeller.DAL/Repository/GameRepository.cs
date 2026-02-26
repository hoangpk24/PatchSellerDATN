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

       
        public async Task<List<Game>> GetAllDetailForPublic(string? keyword)
        {
            try
            {
                var query = await _context.Games
                    .Include(g => g.Publisher)
                    .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                    .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                    .Include(g => g.Patches).ThenInclude(p => p.PatchVersions)
                    .Include(g => g.GameImages)
                    .Where(g => g.Delete != true)
                    .ToListAsync();

                var filtered = query
                    .Where(g =>
                        g.Status == 1 &&
                        g.Publisher != null &&
                        g.Publisher.Delete != true &&
                        g.Publisher.Status == 1 &&
                        g.GameCategories != null &&
                        g.GameCategories.Any(gc =>
                            gc.Delete != true &&
                            gc.Category != null &&
                            gc.Category.Delete != true &&
                            gc.Category.Status == 1));

                if (!string.IsNullOrEmpty(keyword))
                {
                    filtered = filtered.Where(x =>
                        x.Title.Contains(keyword) ||
                        (x.Description != null && x.Description.Contains(keyword)));
                }

                return filtered.ToList();
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
                    .Include(g => g.Patches).ThenInclude(p => p.PatchVersions)
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
                    .Include(g => g.Patches).ThenInclude(p => p.PatchVersions)
                    .Include(g => g.GameImages)
                    .FirstOrDefaultAsync();
                return game;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Game> GetByIdDetailForPublic(int id)
        {
            try
            {
                var game = await _context.Games
                    .Include(g => g.Publisher)
                    .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                    .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                    .Include(g => g.Patches).ThenInclude(p => p.PatchVersions)
                    .Include(g => g.GameImages)
                    .FirstOrDefaultAsync(g => g.GameId == id && g.Delete != true);

                if (game == null)
                {
                    return null;
                }

                var isValid =
                    game.Status == 1 &&
                    game.Publisher != null &&
                    game.Publisher.Delete != true &&
                    game.Publisher.Status == 1 &&
                    game.GameCategories != null &&
                    game.GameCategories.Any(gc =>
                        gc.Delete != true &&
                        gc.Category != null &&
                        gc.Category.Delete != true &&
                        gc.Category.Status == 1);

                return isValid ? game : null;
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
                var duplicateName = await _context.Games.AnyAsync(c => c.Title == game.Title && c.Delete != true);
                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }
                game.Delete = false;
                var added = _context.Games.Add(game).Entity;
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

        public async Task<Game> Update(Game game)
        {
            try
            {
                var existingGame = await _context.Games.FindAsync(game.GameId);

                if (existingGame == null || (existingGame != null && existingGame.Delete == true))
                {
                    throw new InvalidOperationException("NOT_FOUND");
                }

                var duplicateName = await _context.Games.AnyAsync(c => c.GameId != game.GameId && c.Title == game.Title && c.Delete != true);

                if (duplicateName)
                {
                    throw new InvalidOperationException("DUPLICATE_NAME");
                }

                existingGame.Title = game.Title;
                existingGame.Description = game.Description;
                existingGame.Developer = game.Developer;
                existingGame.Thumbnail = game.Thumbnail;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.Status = game.Status;
                existingGame.PublisherId = game.PublisherId;
                existingGame.Delete = game.Delete;

                _context.Games.Update(existingGame);
                await _context.SaveChangesAsync();
                return game;
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
                var game = await _context.Games.FindAsync(id);
                if (game == null || (game != null && game.Delete == true))
                    throw new InvalidOperationException("NOT_FOUND");

                game.Delete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch(InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(List<Game> Data, int TotalCount)> SearchGamesForPublic(
            string? keyword,
            List<int>? categoryIds,
            List<int>? platformIds,
            double? startMoney,
            double? endMoney,
            string? sort,
            int page,
            int perPage)
        {
            try
            {
                var query = _context.Games
                    .Include(g => g.Publisher)
                    .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
                    .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                    .Include(g => g.Patches).ThenInclude(p => p.PatchVersions)
                    .Include(g => g.GameImages)
                    .Where(g => g.Delete != true && g.Status == 1) 
                    .AsQueryable();

                query = query.Where(g =>
                    g.Publisher != null && g.Publisher.Delete != true && g.Publisher.Status == 1 &&
                    g.GameCategories.Any(gc => gc.Delete != true && gc.Category != null &&
                                               gc.Category.Delete != true && gc.Category.Status == 1));

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(g => g.Title.Contains(keyword) ||
                                            (g.Description != null && g.Description.Contains(keyword)));
                }

                if (categoryIds != null && categoryIds.Any())
                {
                    query = query.Where(g => g.GameCategories != null && g.GameCategories.Any(gc => categoryIds.Contains(gc.CategoryId)));
                }

                if (platformIds != null && platformIds.Any())
                {
                    query = query.Where(g => g.GamePlatforms != null && g.GamePlatforms.Any(gp => platformIds.Contains(gp.PlatformId)));
                }

                if (startMoney.HasValue)
                {
                    query = query.Where(g => g.Patches != null && g.Patches.Any(p => p.Price >= startMoney.Value && p.Status == 1 && p.Delete != true));
                }
                if (endMoney.HasValue)
                {
                    query = query.Where(g => g.Patches != null && g.Patches.Any(p => p.Price <= endMoney.Value && p.Status == 1 && p.Delete != true));
                }

                query = sort?.ToLower() switch
                {
                    "name_asc" => query.OrderBy(g => g.Title),
                    "name_desc" => query.OrderByDescending(g => g.Title),
                    "price_asc" => query.OrderBy(g => g.Patches.Where(p => p.Status == 1 && p.Delete != true).Min(p => p.Price)),
                    "price_desc" => query.OrderByDescending(g => g.Patches.Where(p => p.Status == 1 && p.Delete != true).Max(p => p.Price)),
                    _ => query.OrderByDescending(g => g.CreatedAt)
                };

                int totalCount = await query.CountAsync();

                var data = await query
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();

                return (data, totalCount);
            }
            catch (Exception)
            {
                return (null, 0);
            }
        }
    }
}

