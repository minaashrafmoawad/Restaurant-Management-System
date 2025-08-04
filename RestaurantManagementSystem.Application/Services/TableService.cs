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
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllAsync();
        }

        public async Task<Table> GetTableByIdAsync(Guid id)
        {
            return await _tableRepository.GetByIdAsync(id);
        }

        public async Task<Table> CreateTableAsync(Table table)
        {
            if (table.Number <= 0)
            {
                throw new ArgumentException("Table number must be a positive value.");
            }
            var existingTable = await _tableRepository.GetTableByNumberAsync(table.Number);
            if (existingTable != null)
            {
                throw new InvalidOperationException($"Table with number {table.Number} already exists.");
            }
            return await _tableRepository.AddAsync(table);
        }

        public async Task<Table> UpdateTableAsync(Table table)
        {
            if (table.Id == Guid.Empty)
            {
                throw new ArgumentException("Table ID cannot be empty for update.");
            }
            if (table.Number <= 0)
            {
                throw new ArgumentException("Table number must be a positive value.");
            }

            var existingTable = await _tableRepository.GetByIdAsync(table.Id);
            if (existingTable == null)
            {
                throw new InvalidOperationException($"Table with ID {table.Id} not found.");
            }

            // Check if the new table number already exists for a different table
            var tableWithSameNumber = await _tableRepository.GetTableByNumberAsync(table.Number);
            if (tableWithSameNumber != null && tableWithSameNumber.Id != table.Id)
            {
                throw new InvalidOperationException($"Table with number {table.Number} already exists for another table.");
            }

            existingTable.Number = table.Number;
            existingTable.Capacity = table.Capacity;
            existingTable.IsAvailable = table.IsAvailable;
            // You might want to update other auditable fields here if not handled by the base repository

            return await _tableRepository.UpdateAsync(existingTable);
        }

        public async Task DeleteTableAsync(Guid id)
        {
            var tableToDelete = await _tableRepository.GetByIdAsync(id);
            if (tableToDelete == null)
            {
                throw new InvalidOperationException($"Table with ID {id} not found.");
            }
            await _tableRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await _tableRepository.GetAvailableTablesAsync();
        }

        public async Task<Table> GetTableByNumberAsync(int number)
        {
            return await _tableRepository.GetTableByNumberAsync(number);
        }

        public async Task UpdateTableAvailabilityAsync(Guid tableId, bool isAvailable)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
            {
                throw new InvalidOperationException($"Table with ID {tableId} not found.");
            }
            table.IsAvailable = isAvailable;
            await _tableRepository.UpdateAsync(table);
        }
    }
}
