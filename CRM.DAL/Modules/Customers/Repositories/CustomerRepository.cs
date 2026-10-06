using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<Customer>> GetPagedAsync(int page, int pageSize, int? customerTypeId, int? customerStatusId, bool? isActive, string? search)
        {
            var query = _context.Customers
                .AsNoTracking()
                .Include(c => c.CustomerType)
                .Include(c => c.CustomerStatus)
                .Where(c => !c.IsDeleted);

            if (customerTypeId.HasValue)
                query = query.Where(c => c.CustomerTypeId == customerTypeId.Value);

            if (customerStatusId.HasValue)
                query = query.Where(c => c.CustomerStatusId == customerStatusId.Value);

            if (isActive.HasValue)
                query = query.Where(c => c.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(c => c.CustomerNumber.Contains(s) || (c.DisplayName != null && c.DisplayName.Contains(s)));
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.CustomerNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedData<Customer>
            {
                Items = items,
                TotalCount = total
            };
        }

        public async Task<Customer?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(c => c.CustomerType)
                .Include(c => c.CustomerStatus)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Customer?> GetByIdForUpdateAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.CustomerType)
                .Include(c => c.CustomerStatus)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> CustomerNumberExistsAsync(string customerNumber, int? excludeCustomerId = null)
        {
            var q = _context.Customers.AsQueryable();
            if (excludeCustomerId.HasValue)
                q = q.Where(c => c.Id != excludeCustomerId.Value);

            return await q.AnyAsync(c => c.CustomerNumber == customerNumber);
        }

        public async Task<bool> CustomerTypeExistsAsync(int id)
        {
            return await _context.CustomerTypes.AnyAsync(ct => ct.Id == id && ct.IsActive && !ct.IsDeleted);
        }

        public async Task<bool> CustomerStatusExistsAsync(int id)
        {
            return await _context.CustomerStatuses.AnyAsync(cs => cs.Id == id && cs.IsActive && !cs.IsDeleted);
        }
    }
}
