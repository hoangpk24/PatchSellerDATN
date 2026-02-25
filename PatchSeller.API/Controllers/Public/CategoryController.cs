using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/category")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryRepository;

        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }

        [HttpGet("get-all-categories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories(string? keyword)
        {
            try
            {
                List<Category> result = await _categoryRepository.GetAll(keyword);
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
    }
}
