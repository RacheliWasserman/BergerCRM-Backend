using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;
using System.Linq.Expressions;

namespace CRM.DAL.Common.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(
            Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(
            Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entity);
    }
}
