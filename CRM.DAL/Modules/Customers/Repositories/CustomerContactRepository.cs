using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public class CustomerContactRepository : Repository<CustomerContact>, ICustomerContactRepository
    {
        public CustomerContactRepository(CrmDbContext context) : base(context)
        {
        }

        public async Task<List<CustomerContact>> GetActiveLinksForCustomerAsync(int customerId)
        {
            return await _context.CustomerContacts
                .AsNoTracking()
                .Include(cc => cc.Contact)
                .Where(cc => cc.CustomerId == customerId && !cc.IsDeleted && cc.IsActive && !cc.Contact.IsDeleted)
                .OrderByDescending(cc => cc.IsPrimary)
                .ThenBy(cc => cc.Contact.LastName)
                .ThenBy(cc => cc.Contact.FirstName)
                .ToListAsync();
        }

        public async Task<CustomerContact?> FindAnyLinkAsync(int customerId, int contactId)
        {
            return await _context.CustomerContacts
                .FirstOrDefaultAsync(cc => cc.CustomerId == customerId && cc.ContactId == contactId);
        }

        public async Task<CustomerContact?> GetActiveLinkForCustomerContactAsync(int customerId, int contactId)
        {
            return await _context.CustomerContacts
                .FirstOrDefaultAsync(cc => cc.CustomerId == customerId && cc.ContactId == contactId && !cc.IsDeleted && cc.IsActive);
        }

        public async Task<CustomerContact?> GetActivePrimaryForCustomerAsync(int customerId)
        {
            return await _context.CustomerContacts
                .FirstOrDefaultAsync(cc => cc.CustomerId == customerId && !cc.IsDeleted && cc.IsActive && cc.IsPrimary);
        }
    }
}
