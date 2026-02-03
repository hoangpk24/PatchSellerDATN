using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using Pro219.API.DTOs;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/category")]
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

        [HttpGet("get-category-by-id/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var result = await _categoryRepository.GetById(id);
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
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                category.Status = 1;
                category.Delete = false;
                var result = await _categoryRepository.Create(category);

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
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(Constant.ErrorCode.DataRequired);
                }

                var newCategory = new Category
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    Status = category.Status,
                    Delete = category.Delete
                };

                var result = await _categoryRepository.Update(category);

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
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryRepository.Delete(id);

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
