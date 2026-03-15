using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/install-log")]
    [ApiController]
    public class InstallLogController : ControllerBase
    {
        private readonly InstallLogRepository _installLogRepository;

        public InstallLogController()
        {
            _installLogRepository = new InstallLogRepository();
        }

        [HttpGet("get-by-patch-id/{patchId}")]
        public async Task<ActionResult<List<InstallLog>>> GetByPatchId(int patchId)
        {
            try
            {
                var result = await _installLogRepository.GetByPatchId(patchId);
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

        [HttpGet("get-by-user-id/{userId}")]
        public async Task<ActionResult<List<InstallLog>>> GetByUserId(int userId)
        {
            try
            {
                var result = await _installLogRepository.GetByUserId(userId);
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

        [HttpGet("get-by-user-and-patch-id")]
        public async Task<ActionResult<InstallLog>> GetByUserAndPatchId(int userId, int patchId)
        {
            try
            {
                var result = await _installLogRepository.GetByUserAndPatchId(userId, patchId);
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

        [HttpGet("get-by-user-patch-bios")]
        public async Task<ActionResult<InstallLog>> GetByUserPatchBios(int userId, int patchId, string biosSerialNumber)
        {
            try
            {
                var result = await _installLogRepository.GetByUserPatchBios(userId, patchId, biosSerialNumber);
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

        [HttpGet("get-all")]
        public async Task<ActionResult<List<InstallLog>>> GetAll()
        {
            try
            {
                var result = await _installLogRepository.GetAll();
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

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<InstallLog>> GetById(int id)
        {
            try
            {
                var result = await _installLogRepository.GetById(id);
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
        public async Task<ActionResult<InstallLog>> Create([FromBody] InstallLog payload)
        {
            try
            {
                if (payload == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var result = await _installLogRepository.Create(payload);
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
        public async Task<ActionResult<InstallLog>> Update([FromBody] InstallLog payload)
        {
            try
            {
                if (payload == null || payload.Id <= 0)
                {
                    return BadRequest(Constant.ErrorCode.InvalidData);
                }

                var existing = await _installLogRepository.GetById(payload.Id);
                if (existing == null)
                {
                    return NotFound(Constant.ErrorCode.NotFound);
                }

                existing.UserId = payload.UserId;
                existing.PatchId = payload.PatchId;
                existing.PatchVersionId = payload.PatchVersionId;
                existing.BIOSSerialNumber = payload.BIOSSerialNumber;
                existing.InstallDate = payload.InstallDate;

                var result = await _installLogRepository.Update(existing);
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
                var result = await _installLogRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }
    }
}

