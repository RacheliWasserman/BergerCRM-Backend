using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(CrmDbContext context) : base(context)
        {
        }

        public async Task<PagedData<Contact>> GetPagedAsync(int page, int pageSize, bool? isActive, string? search)
        {
            var query = _context.Contacts
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            if (isActive.HasValue)
                query = query.Where(c => c.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(c => c.FirstName.Contains(s) || c.LastName.Contains(s) || (c.Email != null && c.Email.Contains(s)) || (c.Phone != null && c.Phone.Contains(s)));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Contact>
            {
                Items = items,
                TotalCount = total
            };
        }

        public async Task<Contact?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }
    }
}
