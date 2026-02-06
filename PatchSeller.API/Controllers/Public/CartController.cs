using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System.Security.Claims;

namespace PatchSeller.API.Controllers.Public
{
    public class CartController : ControllerBase
    {
        CartRepository _cartRepository;
        CartItemRepository _cartItemRepository;
        public CartController()
        {
            _cartRepository = new CartRepository();
            _cartItemRepository = new CartItemRepository();
        }

        [HttpPost("add-to-cart")]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] CartItemDTO addToCartDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

                if (string.IsNullOrEmpty(userId) || userId != addToCartDTO.UserId.ToString())
                    return StatusCode(403, Constant.ErrorCode.Unauthorized);
                Cart userCart =  await _cartRepository.GetCartByUserId((int.Parse(userId)));

                var listCartItem = await _cartItemRepository.GetAllByUserId(addToCartDTO.UserId);
                if (listCartItem.Where(x => x.PatchId == addToCartDTO.PatchId).FirstOrDefault() != null)
                    return StatusCode(204, Constant.ErrorCode.DataAlreadyExit);               

                CartItem cItem = new CartItem();
                cItem.CartId = userCart.CartId;
                cItem.PatchId = addToCartDTO.PatchId;
                cItem.Delete = false;
                var result = await _cartItemRepository.Create(cItem);
                if(result!=null)
                return Ok(result);
                else return BadRequest(Constant.ErrorCode.DatabaseError);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

        [HttpGet("get-cart-item-by-user-id")]
        public async Task<ActionResult<List<CartItemDetailDTO>>> GetAllCartItemDetailByUserId(int userId)
        {
            try
            {            
                var items = await _cartItemRepository.GetByUserIdWithDetails(userId);
                if (items == null || !items.Any())
                {
                    return NoContent();
                }

                var dtos = items
                    .Where(ci => ci.Patch != null && ci.Patch.Game != null && ci.Patch.Delete != true && ci.Patch.Game.Delete != true)
                    .Select(ci => new CartItemDetailDTO
                    {
                        CartItemId = ci.CartItemId,
                        CartId = ci.CartId,
                        PatchId = ci.Patch.PatchId,
                        PatchName = ci.Patch.Name,
                        GameTitle = ci.Patch.Game.Title,
                        GameId = ci.Patch.Game.GameId,
                        PatchPrice = ci.Patch.Price,
                        PatchDescription = ci.Patch.Description,
                        GameDescription = ci.Patch.Game.Description
                    })
                    .ToList();

                if (!dtos.Any())
                {
                    return NoContent();
                }

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
        }

          [HttpDelete("remove-from-cart")]
          public async Task<ActionResult<bool>> RemoveFromCart( int cartItemId)
          {
            try
            {
                var userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return StatusCode(403, Constant.ErrorCode.Unauthorized);

                var lstItem = await _cartItemRepository.GetAllByUserId(int.Parse(userId));
                if(lstItem.FirstOrDefault(x=>x.CartItemId == cartItemId)==null)
                    { return NoContent(); }
                var result = await _cartItemRepository.Delete(cartItemId);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(Constant.ErrorCode.DatabaseError);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Constant.ErrorCode.OtherError);
            }
          }


        
    }
}
