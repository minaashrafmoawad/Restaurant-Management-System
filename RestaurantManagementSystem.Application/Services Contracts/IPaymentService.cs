using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface IPaymentService
    {
        Task<SalesTransaction> ProcessPaymentAsync(PaymentDto paymentDto);
        Task<SalesTransaction> GetTransactionByOrderIdAsync(Guid orderId);
        Task<IEnumerable<SalesTransaction>> GetTransactionsByDateAsync(DateTime date);
    }
}
