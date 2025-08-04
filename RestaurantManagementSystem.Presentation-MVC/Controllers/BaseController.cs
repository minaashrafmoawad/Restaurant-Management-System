
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Domain.Models;
using RestaurantManagementSystem.Domain.Interfaces; // Assuming IRepository is here
using System;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions; // For Notyf notifications
using RestaurantManagementSystem.Application.Repository_Contracts;

namespace RestaurantManagementSystem.Web.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseEntity
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly INotyfService _notyf;

        public BaseController(IGenericRepository<T> repository, INotyfService notyf)
        {
            _repository = repository;
            _notyf = notyf;
        }

        // GET: /<Controller>/
        public virtual async Task<IActionResult> Index()
        {
            var entities = await _repository.GetAllAsync();
            return View(entities);
        }

        // GET: /<Controller>/Details/5
        public virtual async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id.Value);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // GET: /<Controller>/Create
        public virtual IActionResult Create()
        {
            return View();
        }

        // POST: /<Controller>/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create([Bind()] T entity)
        {
            if (ModelState.IsValid)
            {
                entity.Id = Guid.NewGuid(); // Assign a new GUID for the new entity
                entity.CreatedDate = DateTime.UtcNow;
                entity.IsDeleted = false; // Ensure IsDeleted is false on creation
                await _repository.AddAsync(entity);
                _notyf.Success($"{typeof(T).Name} created successfully!");
                return RedirectToAction(nameof(Index));
            }
            _notyf.Error("Failed to create record. Please check the form.");
            return View(entity);
        }

        // GET: /<Controller>/Edit/5
        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id.Value);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: /<Controller>/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(Guid id, [Bind()] T entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEntity = await _repository.GetByIdAsync(id);
                    if (existingEntity == null)
                    {
                        return NotFound();
                    }

                    // Update properties explicitly to avoid over-posting and maintain CreatedDate/CreatedById
                    // This is a simplified approach; a more robust solution might use AutoMapper or specific DTOs
                    // For demonstration, directly updating here.
                    // You would typically map DTO to entity here.
                    // For example, if T is Category, then:
                    // existingEntity.Name = (entity as Category)?.Name;
                    // existingEntity.Description = (entity as Category)?.Description;

                    // This generic update will work if the entity properties are directly passed.
                    // In a real application, consider a mapping library or specific update methods.
                    entity.CreatedDate = existingEntity.CreatedDate; // Preserve original creation date
                    entity.CreatedById = existingEntity.CreatedById; // Preserve original creator
                    entity.UpdatedDate = DateTime.UtcNow; // Update updated date
                    entity.IsDeleted = existingEntity.IsDeleted; // Preserve IsDeleted status

                    await _repository.UpdateAsync(entity);
                    _notyf.Success($"{typeof(T).Name} updated successfully!");
                }
                catch (Exception ex)
                {
                    _notyf.Error($"An error occurred while updating: {ex.Message}");
                    if (!await EntityExists(entity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _notyf.Error("Failed to update record. Please check the form.");
            return View(entity);
        }

        // GET: /<Controller>/Delete/5
        public virtual async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _repository.GetByIdAsync(id.Value);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: /<Controller>/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id); // Assuming soft delete is handled in the repository
            _notyf.Success($"{typeof(T).Name} deleted successfully!");
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EntityExists(Guid id)
        {
            return await _repository.GetByIdAsync(id) != null;
        }
    }
}

