using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;

namespace Restaurant_Management_System.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<ActionResult<SalesTransaction>> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var transaction = await _paymentService.ProcessPaymentAsync(paymentDto);
                return Ok(transaction);
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

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<SalesTransaction>> GetTransactionByOrder(Guid orderId)
        {
            try
            {
                var transaction = await _paymentService.GetTransactionByOrderIdAsync(orderId);
                if (transaction == null)
                    return NotFound(new { message = "Transaction not found" });

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<SalesTransaction>>> GetTransactionsByDate(DateTime date)
        {
            try
            {
                var transactions = await _paymentService.GetTransactionsByDateAsync(date);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
