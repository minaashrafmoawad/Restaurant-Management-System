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
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            if (category.Id == Guid.Empty)
            {
                throw new ArgumentException("Category ID cannot be empty for update.");
            }
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }
            var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with ID {category.Id} not found.");
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            // You might want to update other auditable fields here if not handled by the base repository

            return await _categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(id);
            if (categoryToDelete == null)
            {
                throw new InvalidOperationException($"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteAsync(id);
        }

        
    }
}
