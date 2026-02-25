using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/patch-version")]
    [ApiController]
    public class PatchVersionController : ControllerBase
    {
        private readonly PatchVersionRepository _patchVersionRepository;

        public PatchVersionController()
        {
            _patchVersionRepository = new PatchVersionRepository();
        }

        private static PatchVersionDetailDTO MapToDetailDTO(PatchVersion? pv)
        {
            if (pv == null || pv.Delete || pv.Status!=1) return null!;
            return new PatchVersionDetailDTO
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
                StaffId = pv.StaffId,
                GameId = pv.Patch?.GameId ?? 0,
                GameName = pv.Patch?.Game?.Title ?? string.Empty,
                PatchName = pv.Patch?.Name ?? string.Empty,
                PatchDescription = pv.Patch?.Description ?? string.Empty,
                UploaderFullName = pv.Staff?.FullName ?? string.Empty,
                PatchImages = pv.PatchImages?
                    .Where(pi => pi.Delete != true)
                    .Select(pi => new PatchImageBasicDTO
                    {
                        PatchImageId = pi.PatchImageId,
                        URL = pi.URL,
                        Name = pi.Name,
                        Description = pi.Description,
                        IsThumbnail = pi.IsThumbnail
                    }).ToList() ?? new List<PatchImageBasicDTO>()
            };
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<PatchVersionDetailDTO>>> GetAll()
        {
            try
            {
                var result = await _patchVersionRepository.GetAllForPublic();
                if (result == null)
                {
                    return Ok(new List<PatchVersionDetailDTO>());
                }
                var dtos = result.Select(MapToDetailDTO)
                                 .Where(dto => dto != null)
                                 .ToList();
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<PatchVersionDetailDTO>> GetById(int id)
        {
            try
            {
                var result = await _patchVersionRepository.GetByIdForPublic(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                var dto = MapToDetailDTO(result);
                if (dto == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-patch-id/{patchId}")]
        public async Task<ActionResult<List<PatchVersionDetailDTO>>> GetByPatchId(int patchId)
        {
            try
            {
                var result = await _patchVersionRepository.GetByPatchIdForPublic(patchId);
                if (result == null)
                {
                    return Ok(new List<PatchVersionDetailDTO>());
                }
                var dtos = result.Select(MapToDetailDTO)
                                 .Where(dto => dto != null)
                                 .ToList();
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-by-game-id/{gameId}")]
        public async Task<ActionResult<List<PatchVersionDetailDTO>>> GetByGameId(int gameId)
        {
            try
            {
                var result = await _patchVersionRepository.GetByGameIdForPublic(gameId);
                if (result == null)
                {
                    return Ok(new List<PatchVersionDetailDTO>());
                }
                var dtos = result.Select(MapToDetailDTO)
                                 .Where(dto => dto != null)
                                 .ToList();
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
        
    }
}
