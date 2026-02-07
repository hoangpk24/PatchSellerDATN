using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/patch-version")]
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
            if (pv == null) return null!;
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
                var result = await _patchVersionRepository.GetAll();
                if (result == null)
                {
                    return NoContent();
                }
                var dtos = result.Select(MapToDetailDTO).ToList();
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
                var result = await _patchVersionRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }
                return Ok(MapToDetailDTO(result));
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
                var result = await _patchVersionRepository.GetByPatchId(patchId);
                if (result == null)
                {
                    return NoContent();
                }
                var dtos = result.Select(MapToDetailDTO).ToList();
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
                var result = await _patchVersionRepository.GetByGameId(gameId);
                if (result == null)
                {
                    return NoContent();
                }
                var dtos = result.Select(MapToDetailDTO).ToList();
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<PatchVersion>> Create([FromBody] PatchVersionCreateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var patchExists = await _patchVersionRepository.PatchExists(dto.PatchId);
                if (!patchExists)
                {
                    return BadRequest(Constant.ErrorCode.InvalidPatchId);
                }

                var versionNameExists = await _patchVersionRepository.VersionNameExists(dto.PatchId, dto.VersionName);
                if (versionNameExists)
                {
                    return BadRequest(Constant.ErrorCode.NameAlreadyExit);
                }

                var staffId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var staffIdInt = int.TryParse(staffId, out var sid) ? sid : dto.StaffId;

                var patchVersion = new PatchVersion
                {
                    WorkWithGameVersion = dto.WorkWithGameVersion,
                    VersionName = dto.VersionName,
                    Links = dto.Links,
                    FileSize = dto.FileSize,
                    ExtractionPassword = dto.ExtractionPassword ?? string.Empty,
                    InstallationGuide = dto.InstallationGuide ?? string.Empty,
                    Changelog = dto.Changelog ?? string.Empty,
                    Status = dto.Status,
                    Note = dto.Note ?? string.Empty,
                    CreateAt = DateTime.UtcNow,
                    Delete = false,
                    PatchId = dto.PatchId,
                    StaffId = staffIdInt
                };

                var result = await _patchVersionRepository.Create(patchVersion);

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
        public async Task<ActionResult<PatchVersion>> Update([FromBody] PatchVersionUpdateDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var existing = await _patchVersionRepository.GetById(dto.PatchVersionId);
                if (existing == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                var patchExists = await _patchVersionRepository.PatchExists(dto.PatchId);
                if (!patchExists)
                {
                    return BadRequest(Constant.ErrorCode.InvalidPatchId);
                }

                var versionNameExists = await _patchVersionRepository.VersionNameExists(dto.PatchId, dto.VersionName, dto.PatchVersionId);
                if (versionNameExists)
                {
                    return BadRequest(Constant.ErrorCode.NameAlreadyExit);
                }

                var staffId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var staffIdInt = int.TryParse(staffId, out var sid) ? sid : dto.StaffId;

                existing.WorkWithGameVersion = dto.WorkWithGameVersion;
                existing.VersionName = dto.VersionName;
                existing.Links = dto.Links;
                existing.FileSize = dto.FileSize;
                existing.ExtractionPassword = dto.ExtractionPassword ?? string.Empty;
                existing.InstallationGuide = dto.InstallationGuide ?? string.Empty;
                existing.Changelog = dto.Changelog ?? string.Empty;
                existing.Status = dto.Status;
                existing.Note = dto.Note ?? string.Empty;
                existing.PatchId = dto.PatchId;
                existing.StaffId = staffIdInt;

                var result = await _patchVersionRepository.Update(existing);

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
                var result = await _patchVersionRepository.Delete(id);

                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }
                return Ok(true);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}
