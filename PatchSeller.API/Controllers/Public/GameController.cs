using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/game")]
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
                        UpdateBy = p.UpdateBy ?? string.Empty,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt,
                        PatchVersions = p.PatchVersions?
                            .Where(pv => pv.Delete != true)
                            .Select(pv => new PatchVersionBasicDTO
                            {
                                PatchVersionId = pv.PatchVersionId,
                                WorkWithGameVersion = pv.WorkWithGameVersion,
                                VersionName = pv.VersionName,
                                Links = pv.Links,
                                FileSize = pv.FileSize,
                                ExtractionPassword = pv.ExtractionPassword,
                                InstallationGuide = pv.InstallationGuide,
                                Changelog = pv.Changelog,
                                Status = pv.Status,
                                Note = pv.Note,
                                CreateAt = pv.CreateAt,
                                Delete = pv.Delete,
                                PatchId = pv.PatchId,
                                GameId = p.GameId
                            }).ToList() ?? new List<PatchVersionBasicDTO>()
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
    }
}
