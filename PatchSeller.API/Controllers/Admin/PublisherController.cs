using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/publisher")]
    [ApiController]

    public class PublisherController : ControllerBase
    {
        PublisherRepository _publisherRepository;

        public PublisherController()
        {
            _publisherRepository = new PublisherRepository();
        }

        [HttpGet("get-all-publishers")]
        public async Task<ActionResult<List<Publisher>>> GetAllPublishers(string? keyword)
        {
            try
            {
                List<Publisher> result = await _publisherRepository.GetAll(keyword);
                if (result == null)
                {
                    return NoContent();
                }

                if(result.Any())
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

        [HttpGet("get-publisher-by-id/{id}")]
        public async Task<ActionResult<Publisher>> GetCategoryById(int id)
        {
            try
            {
                var result = await _publisherRepository.GetById(id);
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
        public async Task<ActionResult<Publisher>> CreatePublisher([FromBody] Publisher category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                category.Status = 1;
                category.Delete = false;
                var result = await _publisherRepository.Create(category);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
                }

                return Ok(result);
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
        public async Task<ActionResult<Publisher>> UpdatePublisher([FromBody] Publisher publisher)
        {
            try
            {
                if (publisher == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newPublisher = new Publisher
                {
                    PublisherId = publisher.PublisherId,
                    Name = publisher.Name,
                    Description = publisher.Description,
                    Status = publisher.Status,
                    Delete = publisher.Delete
                };

                var result = await _publisherRepository.Update(publisher);

                if (result == null)
                {
                    return StatusCode(500, Constant.ErrorCode.DatabaseError);
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
        public async Task<ActionResult<bool>> DeletePublisher(int id)
        {
            try
            {
                var result = await _publisherRepository.Delete(id);

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
