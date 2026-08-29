using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync<U>(U Id);

        public Task AddAsync(T entity);
        public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
