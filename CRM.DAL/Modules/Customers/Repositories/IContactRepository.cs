using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<PagedData<Contact>> GetPagedAsync(int page, int pageSize, bool? isActive, string? search);

        Task<Contact?> GetByIdForUpdateAsync(int id);
    }
}
