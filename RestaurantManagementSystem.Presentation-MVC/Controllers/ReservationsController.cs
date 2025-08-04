using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;

namespace Restaurant_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ITableReservationService _reservationService;

        public ReservationsController(ITableReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<ActionResult<TableReservation>> CreateReservation([FromBody] TableReservationDto reservationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reservation = await _reservationService.CreateReservationAsync(reservationDto);
                return CreatedAtAction(nameof(GetReservationsByDate),
                    new { date = reservation.Date.ToString("yyyy-MM-dd") }, reservation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<TableReservation>>> GetReservationsByDate(DateTime date)
        {
            try
            {
                var reservations = await _reservationService.GetReservationsByDateAsync(date);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> CheckTableAvailability(
            [FromQuery] int tableNumber,
            [FromQuery] DateTime date,
            [FromQuery] TimeSpan time)
        {
            try
            {
                var isAvailable = await _reservationService.IsTableAvailableAsync(tableNumber, date, time);
                return Ok(new { isAvailable });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelReservation(Guid id)
        {
            try
            {
                await _reservationService.CancelReservationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
