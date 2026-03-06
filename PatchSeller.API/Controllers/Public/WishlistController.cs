using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly WishlistRepository _wishlistRepository;
        private readonly GameRepository _gameRepository;

        public WishlistController()
        {
            _wishlistRepository = new WishlistRepository();
            _gameRepository = new GameRepository();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Wishlist>>> GetAll()
        {
            try
            {
                var result = await _wishlistRepository.GetAll();
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-user-id")]
        public async Task<ActionResult<List<Wishlist>>> GetByUserId(int userId)
        {
            try
            {
                var result = await _wishlistRepository.GetAllByUserId(userId);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-customer-and-game/{customerId}/{gameId}")]
        public async Task<ActionResult<Wishlist>> GetWishlistByCustomerAndGame(int customerId, int gameId)
        {
            try
            {
                var result = await _wishlistRepository.GetWishlistByCustomerAndGame(customerId, gameId);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }


        [HttpGet("get-game-detail-by-user-id")]
        public async Task<ActionResult<List<GameDetailDTO>>> GetGameDetailsByUserId(int userId)
        {
            try
            {
                var wishlists = await _wishlistRepository.GetAllByUserId(userId);
                if (wishlists == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                var gameIds = wishlists
                    .Where(w => w.GameId > 0)
                    .Select(w => w.GameId)
                    .Distinct()
                    .ToList();

                if (!gameIds.Any())
                {
                    return Ok(new List<GameDetailDTO>());
                }

                var result = new List<GameDetailDTO>();

                foreach (var gameId in gameIds)
                {
                    var game = await _gameRepository.GetByIdDetailForPublic(gameId);
                    if (game == null)
                    {
                        continue;
                    }

                    var dto = MapToGameDetailDTO(game);
                    if (dto != null)
                    {
                        result.Add(dto);
                    }
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<Wishlist>> GetById(int id)
        {
            try
            {
                var result = await _wishlistRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Wishlist>> Create([FromBody] Wishlist wishlist)
        {
            try
            {
                if (wishlist == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userIdFromToken))
                {
                    if (wishlist.UserId != userIdFromToken)
                    {
                        return StatusCode(403, Constant.ErrorCode.Unauthorized);
                    }
                }

                var result = await _wishlistRepository.Create(wishlist);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Wishlist>> Update([FromBody] Wishlist wishlist)
        {
            try
            {
                if (wishlist == null || wishlist.Id <= 0)
                {
                    return BadRequest(Constant.ErrorCode.InvalidData);
                }

                var existing = await _wishlistRepository.GetById(wishlist.Id);
                if (existing == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                existing.GameId = wishlist.GameId;
                existing.UserId = wishlist.UserId;

                var result = await _wishlistRepository.Update(existing);
                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _wishlistRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        private static GameDetailDTO? MapToGameDetailDTO(Game game)
        {
            if (game == null) return null;

            if (game.Delete == true || game.Status != 1)
            {
                return null;
            }

            if (game.Publisher == null || game.Publisher.Delete == true || game.Publisher.Status != 1)
            {
                return null;
            }

            var validGameCategories = game.GameCategories?
                .Where(gc => gc.Delete != true &&
                             gc.Category != null &&
                             gc.Category.Delete != true &&
                             gc.Category.Status == 1)
                .ToList() ?? new List<GameCategory>();

            if (!validGameCategories.Any())
            {
                return null;
            }

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
                    .Where(gp => gp.Delete != true &&
                                 gp.Platform != null &&
                                 gp.Platform.Delete != true)
                    .Select(gp => new PlatformBasicDTO
                    {
                        PlatformId = gp.Platform.PlatformId,
                        Name = gp.Platform.Name,
                        Description = gp.Platform.Description,
                        Status = gp.Platform.Status
                    }).ToList() ?? new List<PlatformBasicDTO>(),
                Categories = validGameCategories
                    .Select(gc => new CategoryBasicDTO
                    {
                        CategoryId = gc.Category.CategoryId,
                        CategoryName = gc.Category.CategoryName,
                        Description = gc.Category.Description,
                        Status = gc.Category.Status
                    }).ToList() ?? new List<CategoryBasicDTO>(),
                Patches = game.Patches?
                    .Where(p =>
                        p.Delete != true &&
                        p.Status == 1)
                    .Select(p => new PatchBasicDTO
                    {
                        PatchId = p.PatchId,
                        Name = p.Name,
                        Price = p.Price,
                        ThumbnailLink = p.ThumbnailLink,
                        Description = p.Description,
                        UpdateBy = p.UpdateBy ?? string.Empty,
                        Status = p.Status,
                        CreatedAt = p.CreatedAt,
                        PatchVersions = p.PatchVersions?
                            .Where(pv => pv.Delete != true && pv.Status == 1)
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
    }
}

