using System.Threading;
using System.Threading.Tasks;
using CRM.DAL.Data;

namespace CRM.DAL.Common.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CrmDbContext _context;

        public UnitOfWork(CrmDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
