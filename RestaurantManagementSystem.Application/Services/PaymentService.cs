using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Repository_Contracts;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ISalesTransactionRepository _transactionRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(ISalesTransactionRepository transactionRepository, IOrderRepository orderRepository)
        {
            _transactionRepository = transactionRepository;
            _orderRepository = orderRepository;
        }

        public async Task<SalesTransaction> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            var order = await _orderRepository.GetByIdAsync(paymentDto.OrderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            var transaction = new SalesTransaction
            {
                Id = Guid.NewGuid(),
                OrderId = paymentDto.OrderId,
                Amount = order.TotalAmount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentReference = paymentDto.PaymentReference,
                CreatedDate = DateTime.UtcNow
            };

            return await _transactionRepository.AddAsync(transaction);
        }

        public async Task<SalesTransaction> GetTransactionByOrderIdAsync(Guid orderId)
        {
            var transactions = await _transactionRepository.FindAsync(t => t.OrderId == orderId);
            return transactions.FirstOrDefault();
        }

        public async Task<IEnumerable<SalesTransaction>> GetTransactionsByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            return await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
        }
    }

}
