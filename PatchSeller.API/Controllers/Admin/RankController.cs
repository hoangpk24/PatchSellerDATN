using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/rank")]
    [ApiController]

    public class RankController : ControllerBase
    {
        RankRepository _rankRepository;

        public RankController()
        {
            _rankRepository = new RankRepository();
        }

        [HttpGet("get-all-ranks")]
        public async Task<ActionResult<List<Rank>>> GetAllRanks(string? keyword)
        {
            try
            {
                List<Rank> result = await _rankRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-rank-by-id/{id}")]
        public async Task<ActionResult<Rank>> GetRankById(int id)
        {
            try
            {
                var result = await _rankRepository.GetById(id);
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

        [HttpPost("create")]
        public async Task<ActionResult<Rank>> CreateRank([FromBody] Rank rank)
        {
            try
            {
                if (rank == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                rank.Status = 1;
                rank.Delete = false;
                var result = await _rankRepository.Create(rank);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_RANK_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_RANK_POINT")
            {
                return BadRequest(Constant.ErrorCode.PointAlreadyExit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<Rank>> UpdateRank([FromBody] Rank rank)
        {
            try
            {
                if (rank == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newRank = new Rank
                {
                    RankId = rank.RankId,
                    RankName = rank.RankName,
                    Description = rank.Description,
                    MiniumSpend = rank.MiniumSpend,
                    Status = rank.Status,
                    Delete = rank.Delete
                };

                var result = await _rankRepository.Update(rank);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_RANK_NAME")
            {
                return BadRequest(Constant.ErrorCode.NameAlreadyExit);
            }
            catch (InvalidOperationException ex) when (ex.Message == "DUPLICATE_RANK_POINT")
            {
                return BadRequest(Constant.ErrorCode.PointAlreadyExit);
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
        public async Task<ActionResult<bool>> DeleteRank(int id)
        {
            try
            {
                var result = await _rankRepository.Delete(id);

                if (!result)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
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
