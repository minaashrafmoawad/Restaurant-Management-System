using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Services_Contracts
{
    public interface ITableReservationService
    {
        Task<TableReservation> CreateReservationAsync(TableReservationDto reservationDto);
        Task<IEnumerable<TableReservation>> GetReservationsByDateAsync(DateTime date);
        Task<bool> IsTableAvailableAsync(int tableNumber, DateTime date, TimeSpan time);
        Task<TableReservation> CancelReservationAsync(Guid reservationId);
    }
}
