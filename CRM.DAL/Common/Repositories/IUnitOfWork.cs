using System.Threading;
using System.Threading.Tasks;

namespace CRM.DAL.Common.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
