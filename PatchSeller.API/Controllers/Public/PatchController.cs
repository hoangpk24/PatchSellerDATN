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
            return new PatchDetailDTO
            {
                PatchId = patch.PatchId,
                Name = patch.Name,
                Price = patch.Price,
                Description = patch.Description,
                UpdateBy = patch.UpdateBy,
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
                    .Where(pv => pv.Delete != true)
                    .Select(pv => new PatchVersionBasicDTO
                    {
                        PatchVersionId = pv.PatchVersionId,
                        WorkWithGameVersion = pv.WorkWithGameVersion,
                        VersionName = pv.VersionName,
                        FileUrl = pv.FileUrl,
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
                    result = result.OrderByDescending(c => c.CreatedAt).Where(x=>x.Status==1).ToList();
                }
                return Ok(result);
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
                    result = result.OrderByDescending(c => c.CreatedAt).Where(x=>x.Status==1).ToList();
                }
                var dtos = result.Select(MapToPatchDetailDTO).ToList();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }


    }
}
