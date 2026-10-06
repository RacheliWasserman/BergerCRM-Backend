using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllActiveAsync(System.Threading.CancellationToken cancellationToken = default);

        Task<User?> GetByIdAsync(int id);
    }
}
