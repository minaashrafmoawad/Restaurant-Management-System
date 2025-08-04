using RestaurantManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Application.Repository_Contracts
{

    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task DeleteAsync(Guid id);
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }


}


