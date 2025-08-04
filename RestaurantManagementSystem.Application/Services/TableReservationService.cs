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
    public class TableReservationService : ITableReservationService
    {
        private readonly ITableReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;

        public TableReservationService(ITableReservationRepository reservationRepository, ITableRepository tableRepository)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
        }

        public async Task<TableReservation> CreateReservationAsync(TableReservationDto reservationDto)
        {
            var table = await _tableRepository.GetTableByNumberAsync(reservationDto.TableNumber);
            if (table == null)
                throw new ArgumentException("Table not found");

            if (!await IsTableAvailableAsync(reservationDto.TableNumber, reservationDto.Date, reservationDto.Time))
                throw new InvalidOperationException("Table is not available at the requested time");

            var reservation = new TableReservation
            {
                Id = Guid.NewGuid(),
                TableId = table.Id,
                CustomerId = reservationDto.CustomerId,
                Date = reservationDto.Date,
                Time = reservationDto.Time,
                CreatedDate = DateTime.UtcNow
            };

            return await _reservationRepository.AddAsync(reservation);
        }

        public async Task<IEnumerable<TableReservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _reservationRepository.GetReservationsByDateAsync(date);
        }

        public async Task<bool> IsTableAvailableAsync(int tableNumber, DateTime date, TimeSpan time)
        {
            var table = await _tableRepository.GetTableByNumberAsync(tableNumber);
            if (table == null || !table.IsAvailable)
                return false;

            return await _reservationRepository.IsTableAvailableAsync(table.Id, date, time);
        }

        public async Task<TableReservation> CancelReservationAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation != null)
            {
                reservation.IsDeleted = true;
                reservation.UpdatedDate = DateTime.UtcNow;
                await _reservationRepository.UpdateAsync(reservation);
            }
            return reservation;
        }
    }
}
