using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Application.Services_Contracts;

namespace Restaurant_Management_System.Controllers
{
    public class MenuViewController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuViewController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _menuService.GetCategoriesWithItemsAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
                return NotFound();

            return View(menuItem);
        }

        public async Task<IActionResult> Category(Guid categoryId)
        {
            var categories = await _menuService.GetCategoriesWithItemsAsync();
            var category = categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
                return NotFound();

            return View(category);
        }
    }
}
