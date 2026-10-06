using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Data;
using CRM.DAL.Modules.Identity.Entities;

namespace CRM.DAL.Modules.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CrmDbContext _context;

        public UserRepository(CrmDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllActiveAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => !u.IsDeleted && u.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }
    }
}
