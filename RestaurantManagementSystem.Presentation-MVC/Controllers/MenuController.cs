using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;

namespace Restaurant_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesWithItems()
        {
            try
            {
                var categories = await _menuService.GetCategoriesWithItemsAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAvailableItems()
        {
            try
            {
                var items = await _menuService.GetAvailableMenuItemsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("items/{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(Guid id)
        {
            try
            {
                var item = await _menuService.GetMenuItemByIdAsync(id);
                if (item == null)
                    return NotFound(new { message = "Menu item not found" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("items")]
        public async Task<ActionResult<MenuItem>> CreateMenuItem([FromBody] MenuItem menuItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdItem = await _menuService.CreateMenuItemAsync(menuItem);
                return CreatedAtAction(nameof(GetMenuItem), new { id = createdItem.Id }, createdItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("items/{id}")]
        public async Task<ActionResult<MenuItem>> UpdateMenuItem(Guid id, [FromBody] MenuItem menuItem)
        {
            try
            {
                if (id != menuItem.Id)
                    return BadRequest(new { message = "ID mismatch" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedItem = await _menuService.UpdateMenuItemAsync(menuItem);
                return Ok(updatedItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("items/{id}")]
        public async Task<ActionResult> DeleteMenuItem(Guid id)
        {
            try
            {
                await _menuService.DeleteMenuItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("items/{id}/availability")]
        public async Task<ActionResult> UpdateAvailability(Guid id, [FromBody] bool isAvailable)
        {
            try
            {
                await _menuService.UpdateItemAvailabilityAsync(id, isAvailable);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
