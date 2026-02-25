using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/patch")]
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

            if (patch.Delete == true)
            {
                return null;
            }

            return new PatchDetailDTO
            {
                PatchId = patch.PatchId,
                Name = patch.Name,
                Price = patch.Price,
                Description = patch.Description,
                UpdateBy = patch.UpdateBy ?? string.Empty,
                Status = patch.Status,
                CreatedAt = patch.CreatedAt,
                GameId = patch.GameId,
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
                        GameId = patch.GameId
                    }).ToList() ?? new List<PatchVersionBasicDTO>()
            };
        }

        [HttpGet("get-all-patches")]
        public async Task<ActionResult<List<Patch>>> GetAllPatches(string? keyword)
        {
            try
            {
                List<Patch> result = await _patchRepository.GetAllForPublic(keyword);
                if (result == null)
                {
                    
                    return Ok(new List<Patch>());
                }
                if (result.Any())
                {
                    result = result
                        .Where(x => x.Status == 1 && x.Delete != true)
                        .OrderByDescending(c => c.CreatedAt)
                        .ToList();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-patch-by-id/{id}")]
        public async Task<ActionResult<Patch>> GetPatchById(int id)
        {
            try
            {
                var result = await _patchRepository.GetByIdForPublic(id);
                if (result == null || result.Delete == true || result.Status != 1)
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
                List<Patch> result = await _patchRepository.GetByGameIdForPublic(gameId);
                if (result == null)
                {
                    return Ok(new List<PatchDetailDTO>());
                }
                if (result.Any())
                {
                    result = result
                        .Where(x => x.Status == 1 && x.Delete != true)
                        .OrderByDescending(c => c.CreatedAt)
                        .ToList();
                }
                var dtos = result
                    .Select(MapToPatchDetailDTO)
                    .Where(dto => dto != null)
                    .ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }


    }
}
