using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameRepository _gameRepository;
        private readonly GameCategoryRepository _gameCategoryRepository;
        private readonly GamePlatformRepository _gamePlatformRepository;

        public GameController()
        {
            _gameRepository = new GameRepository();
            _gameCategoryRepository = new GameCategoryRepository();
            _gamePlatformRepository = new GamePlatformRepository();
        }

        private static GameDetailDTO MapToGameDetailDTO(Game game)
        {
            if (game == null) return null;
            return new GameDetailDTO
            {
                GameId = game.GameId,
                Title = game.Title,
                Developer = game.Developer,
                Description = game.Description,
                Thumbnail = game.Thumbnail,
                ReleaseDate = game.ReleaseDate,
                Status = game.Status,
                CreatedAt = game.CreatedAt,
                PublisherId = game.PublisherId,
                Publisher = game.Publisher == null ? null : new PublisherBasicDTO
                {
                    PublisherId = game.Publisher.PublisherId,
                    Name = game.Publisher.Name,
                    Description = game.Publisher.Description,
                    Status = game.Publisher.Status
                },
                Platforms = game.GamePlatforms?
                    .Where(gp => gp.Delete != true && gp.Platform != null && gp.Platform.Delete != true)
                    .Select(gp => new PlatformBasicDTO
                    {
                        PlatformId = gp.Platform.PlatformId,
                        Name = gp.Platform.Name,
                        Description = gp.Platform.Description,
                        Status = gp.Platform.Status
                    }).ToList() ?? new List<PlatformBasicDTO>(),
                Categories = game.GameCategories?
                    .Where(gc => gc.Delete != true && gc.Category != null && gc.Category.Delete != true)
                    .Select(gc => new CategoryBasicDTO
                    {
                        CategoryId = gc.Category.CategoryId,
                        CategoryName = gc.Category.CategoryName,
                        Description = gc.Category.Description,
                        Status = gc.Category.Status
                    }).ToList() ?? new List<CategoryBasicDTO>(),
                Patches = game.Patches?
                    .Where(p => p.Delete != true)
                    .Select(p => new PatchBasicDTO
                    {
                        PatchId = p.PatchId,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        UpdateBy = p.UpdateBy,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt
                    }).ToList() ?? new List<PatchBasicDTO>(),
                GameImages = game.GameImages?
                    .Where(gi => gi.Delete != true)
                    .Select(gi => new GameImageBasicDTO
                    {
                        GameImageId = gi.GameImageId,
                        URL = gi.URL,
                        Name = gi.Name,
                        Description = gi.Description,
                        Status = gi.Status
                    }).ToList() ?? new List<GameImageBasicDTO>()
            };
        }

        [HttpGet("get-all-games")]
        public async Task<ActionResult<List<Game>>> GetAllGames(string? keyword)
        {
            try
            {
                List<Game> result = await _gameRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }

                if (result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-game-by-id/{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            try
            {
                var result = await _gameRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-all-games-detail")]
        public async Task<ActionResult<List<GameDetailDTO>>> GetAllGamesDetail(string? keyword)
        {
            try
            {
                List<Game> result = await _gameRepository.GetAllDetail(keyword);
                if (result == null)
                {
                    return NoContent();
                }
                if (result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }
                var dtos = result.Select(MapToGameDetailDTO).ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-id-detail/{id}")]
        public async Task<ActionResult<GameDetailDTO>> GetGameByIdDetail(int id)
        {
            try
            {
                var result = await _gameRepository.GetByIdDetail(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                var dto = MapToGameDetailDTO(result);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<GameDetailDTO>> CreateGame([FromBody] GameCreateEditDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var game = new Game
                {
                    Title = dto.Title ?? string.Empty,
                    Developer = dto.Developer ?? string.Empty,
                    Description = dto.Description ?? string.Empty,
                    Thumbnail = dto.Thumbnail ?? string.Empty,
                    ReleaseDate = dto.ReleaseDate,
                    Status = dto.Status > 0 ? dto.Status : 1,
                    Delete = false,
                    CreatedAt = dto.CreatedAt ?? DateTime.UtcNow,
                    PublisherId = dto.PublisherId >= 0 ? dto.PublisherId : -1
                };

                var result = await _gameRepository.Create(game);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                if (dto.CategoryIds != null && dto.CategoryIds.Any())
                {
                    foreach (var categoryId in dto.CategoryIds)
                    {
                        await _gameCategoryRepository.Create(new GameCategory
                        {
                            GameId = result.GameId,
                            CategoryId = categoryId,
                            Delete = false
                        });
                    }
                }

                if (dto.Platforms != null && dto.Platforms.Any())
                {
                    foreach (var platform in dto.Platforms)
                    {
                        await _gamePlatformRepository.Create(new GamePlatform
                        {
                            GameId = result.GameId,
                            PlatformId = platform.PlatformId,
                            Description = platform.Description ?? string.Empty,
                            Status = 1,
                            Delete = false
                        });
                    }
                }

                var createdDetail = await _gameRepository.GetByIdDetail(result.GameId);
                return Ok(createdDetail != null ? MapToGameDetailDTO(createdDetail) : result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Game>> UpdateGame([FromBody] GameCreateEditDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var game = new Game
                {
                    GameId = dto.GameId,
                    Title = dto.Title,
                    Developer = dto.Developer,
                    Description = dto.Description,
                    Thumbnail = dto.Thumbnail,
                    ReleaseDate = dto.ReleaseDate,
                    Status = dto.Status > 0 ? dto.Status : 1,
                    Delete = dto.Delete,
                    PublisherId = dto.PublisherId >= 0 ? dto.PublisherId : -1
                };

                var result = await _gameRepository.Update(game);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                var allExistingCategories = await _gameCategoryRepository.GetByGameId(result.GameId, false);

                var incomingCategoryIds = dto.CategoryIds ?? new List<int>();

                var toDelete = allExistingCategories
                    .Where(db => db.Delete != true && !incomingCategoryIds.Contains(db.CategoryId))
                    .ToList();

                foreach (var item in toDelete)
                {
                    item.Delete = true;
                    await _gameCategoryRepository.Update(item);
                }

                foreach (var catId in incomingCategoryIds)
                {
                    var existingRecord = allExistingCategories.FirstOrDefault(x => x.CategoryId == catId);

                    if (existingRecord == null)
                    {
                        await _gameCategoryRepository.Create(new GameCategory
                        {
                            GameId = result.GameId,
                            CategoryId = catId,
                            Delete = false
                        });
                    }
                    else if (existingRecord.Delete == true)
                    {
                        existingRecord.Delete = false;
                        await _gameCategoryRepository.Update(existingRecord);
                    }
                }

                var allExistingPlatforms = await _gamePlatformRepository.GetByGameId(result.GameId);
                var incomingPlatformIds = dto.Platforms?.Select(p => p.PlatformId).ToList() ?? new List<int>();

                var platformsToHide = allExistingPlatforms
                    .Where(db => db.Delete != true && !incomingPlatformIds.Contains(db.PlatformId))
                    .ToList();

                foreach (var item in platformsToHide)
                {
                    item.Delete = true;
                    await _gamePlatformRepository.Update(item);
                }

                if (dto.Platforms != null)
                {
                    foreach (var platDto in dto.Platforms)
                    {
                        var existingRecord = allExistingPlatforms.FirstOrDefault(x => x.PlatformId == platDto.PlatformId);

                        if (existingRecord == null)
                        {
                            await _gamePlatformRepository.Create(new GamePlatform
                            {
                                GameId = result.GameId,
                                PlatformId = platDto.PlatformId,
                                Description = platDto.Description ?? string.Empty,
                                Status = 1,
                                Delete = false
                            });
                        }
                        else
                        {
                            existingRecord.Description = platDto.Description ?? string.Empty;
                            existingRecord.Delete = false;
                            await _gamePlatformRepository.Update(existingRecord);
                        }
                    }
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteGame(int id)
        {
            try
            {
                var result = await _gameRepository.Delete(id);

                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                var categories = await _gameCategoryRepository.GetByGameId(id);
                if (categories != null && categories.Any())
                {
                    foreach (var cat in categories.Where(c => c.Delete != true))
                    {
                        cat.Delete = true;
                        await _gameCategoryRepository.Update(cat);
                    }
                }

                var platforms = await _gamePlatformRepository.GetByGameId(id);
                if (platforms != null && platforms.Any())
                {
                    foreach (var plat in platforms.Where(p => p.Delete != true))
                    {
                        plat.Delete = true;
                        await _gamePlatformRepository.Update(plat);
                    }
                }

                return Ok(true);
            }
            catch (InvalidOperationException ex) when (ex.Message == "NOT_FOUND")
            {
                return BadRequest(Constant.ErrorCode.NotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
