using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Services_Contracts;

namespace Restaurant_Management_System.Controllers
{
    public class ReservationViewController : Controller
    {
        private readonly ITableReservationService _reservationService;

        public ReservationViewController(ITableReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TableReservationDto reservationDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reservation = await _reservationService.CreateReservationAsync(reservationDto);
                    TempData["Success"] = "Reservation created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(reservationDto);
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var selectedDate = date ?? DateTime.Today;
            var reservations = await _reservationService.GetReservationsByDateAsync(selectedDate);

            ViewBag.SelectedDate = selectedDate;
            return View(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CheckAvailability(int tableNumber, DateTime date, TimeSpan time)
        {
            var isAvailable = await _reservationService.IsTableAvailableAsync(tableNumber, date, time);
            return Json(new { isAvailable });
        }
    }
}
