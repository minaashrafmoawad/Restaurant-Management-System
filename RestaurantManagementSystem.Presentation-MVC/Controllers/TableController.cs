using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.Services_Contracts;
using RestaurantManagementSystem.Domain.Models;

namespace RestaurantManagementSystem.Web.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // GET: Table
        public async Task<IActionResult> Index()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return View(tables);
        }

        // GET: Table/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // GET: Table/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Table/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Table table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if table number already exists
                    var existingTable = await _tableService.GetTableByNumberAsync(table.Number);
                    if (existingTable != null)
                    {
                        ModelState.AddModelError("Number", "A table with this number already exists.");
                        return View(table);
                    }

                    await _tableService.CreateTableAsync(table);
                    TempData["SuccessMessage"] = "Table created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the table: " + ex.Message);
                }
            }
            return View(table);
        }

        // GET: Table/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: Table/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if table number already exists (excluding current table)
                    var existingTable = await _tableService.GetTableByNumberAsync(table.Number);
                    if (existingTable != null && existingTable.Id != table.Id)
                    {
                        ModelState.AddModelError("Number", "A table with this number already exists.");
                        return View(table);
                    }

                    await _tableService.UpdateTableAsync(table);
                    TempData["SuccessMessage"] = "Table updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the table: " + ex.Message);
                }
            }
            return View(table);
        }

        // GET: Table/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        // POST: Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _tableService.DeleteTableAsync(id);
                TempData["SuccessMessage"] = "Table deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the table: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Table/UpdateAvailability
        [HttpPost]
        public async Task<IActionResult> UpdateAvailability(Guid id, bool isAvailable)
        {
            try
            {
                await _tableService.UpdateTableAvailabilityAsync(id, isAvailable);
                return Json(new { success = true, message = "Table availability updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating table availability: " + ex.Message });
            }
        }

        // GET: Table/Available
        public async Task<IActionResult> Available()
        {
            var availableTables = await _tableService.GetAvailableTablesAsync();
            return View(availableTables);
        }
    }
}