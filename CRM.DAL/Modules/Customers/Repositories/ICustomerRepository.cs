using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<PagedData<Customer>> GetPagedAsync(int page, int pageSize, int? customerTypeId, int? customerStatusId, bool? isActive, string? search);

        Task<Customer?> GetByIdWithDetailsAsync(int id);

        Task<Customer?> GetByIdForUpdateAsync(int id);

        Task<bool> CustomerNumberExistsAsync(string customerNumber, int? excludeCustomerId = null);

        Task<bool> CustomerTypeExistsAsync(int id);

        Task<bool> CustomerStatusExistsAsync(int id);
    }
}
