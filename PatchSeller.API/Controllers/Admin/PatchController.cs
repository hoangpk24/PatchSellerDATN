using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/patch")]
    [ApiController]
    public class PatchController : ControllerBase
    {
        PatchRepository _patchRepository;

        public PatchController()
        {
            _patchRepository = new PatchRepository();
        }

        private static PatchDetailDTO MapToPatchDetailDTO(Patch patch)
        {
            if (patch == null) return null;
            return new PatchDetailDTO
            {
                PatchId = patch.PatchId,
                Name = patch.Name,
                Price = patch.Price,
                Description = patch.Description,
                UpdateBy = patch.UpdateBy,
                Status = patch.Status,
                CreatedAt = patch.CreatedAt,
                Game = patch.Game == null ? null : new GameDTO
                {
                    GameId = patch.Game.GameId,
                    Title = patch.Game.Title,
                    Developer = patch.Game.Developer,
                    Description = patch.Game.Description,
                    CreatedAt = patch.Game.CreatedAt,
                    Thumbnail = patch.Game.Thumbnail,
                    ReleaseDate = patch.Game.ReleaseDate,
                    Status = patch.Game.Status,
                    Delete = patch.Game.Delete
                },
                PatchImages = patch.PatchImages?
                    .Where(pi => pi.Delete != true)
                    .Select(pi => new PatchImageBasicDTO
                    {
                        PatchImageId = pi.PatchImageId,
                        URL = pi.URL,
                        Name = pi.Name,
                        Description = pi.Description,
                        IsThumbnail = pi.IsThumbnail
                    }).ToList() ?? new List<PatchImageBasicDTO>(),
                PatchVersions = patch.PatchVersions?
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
                        CreateAt = pv.CreateAt
                    }).ToList() ?? new List<PatchVersionBasicDTO>()
            };
        }

        [HttpGet("get-all-patches")]
        public async Task<ActionResult<List<Patch>>> GetAllPatches(string? keyword)
        {
            try
            {
                List<Patch> result = await _patchRepository.GetAll(keyword);
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

        [HttpGet("get-all-patches-detail")]
        public async Task<ActionResult<List<PatchDetailDTO>>> GetAllGamesDetail(string? keyword)
        {
            try
            {
                List<Patch> result = await _patchRepository.GetAllDetail(keyword);
                if (result == null)
                {
                    return NoContent();
                }
                if (result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }
                var dtos = result.Select(MapToPatchDetailDTO).ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-patch-by-id/{id}")]
        public async Task<ActionResult<Patch>> GetPatchById(int id)
        {
            try
            {
                var result = await _patchRepository.GetById(id);
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

        [HttpGet("get-patch-by-game-id/{gameId}")]
        public async Task<ActionResult<List<PatchDetailDTO>>> GetPatchByGameId(int gameId)
        {
            try
            {
                List<Patch> result = await _patchRepository.GetByGameId(gameId);
                if (result == null)
                {
                    return NoContent();
                }
                if (result.Any())
                {
                    result = result.OrderByDescending(c => c.CreatedAt).ToList();
                }
                var dtos = result.Select(MapToPatchDetailDTO).ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Patch>> CreatePatch([FromBody] PatchCreateUpdateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var patch = new Patch
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    GameId = dto.GameId,
                    CreatedAt = dto.CreatedAt ?? DateTime.UtcNow,
                    Status = 1,
                    Delete = false,
                    UpdateBy = dto.UpdateBy ?? userId
                };

                var result = await _patchRepository.Create(patch);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Patch>> UpdatePatch([FromBody] PatchCreateUpdateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var patch = new Patch
                {
                    PatchId = dto.PatchId,
                    Name = dto.Name,
                    Price = dto.Price,
                    Description = dto.Description,
                    GameId = dto.GameId,
                    CreatedAt = dto.CreatedAt,
                    Status = dto.Status,
                    Delete = dto.Delete,
                    UpdateBy = dto.UpdateBy ?? userId
                };

                var result = await _patchRepository.Update(patch);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeletePatch(int id)
        {
            try
            {
                var result = await _patchRepository.Delete(id);

                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
