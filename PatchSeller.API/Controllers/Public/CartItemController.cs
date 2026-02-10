using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    [Route("/cart-item")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        CartItemRepository _cartItemRepository;

        public CartItemController()
        {
            _cartItemRepository = new CartItemRepository();
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(int id)
        {
            try
            {
                var result = await _cartItemRepository.DeleteCartItem(id);
                if (result == null)
                {
                    return NotFound(Constant.ErrorCode.DataNotFound);
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
