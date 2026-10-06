using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Projects.Entities;
using System.Collections.Generic;

namespace CRM.DAL.Modules.Projects.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<PagedData<Unit>> GetPagedAsync(int page, int pageSize, int? projectId, int? buildingId, int? floorId, int? unitStatusId, int? unitTypeId, string? search, System.Threading.CancellationToken cancellationToken = default)
        {
            var query = _context.Units
                .AsNoTracking()
                .Include(u => u.Floor).ThenInclude(f => f.Building).ThenInclude(b => b.Project)
                .Include(u => u.UnitType)
                .Include(u => u.UnitStatus)
                .Where(u => !u.IsDeleted);

            if (projectId.HasValue) query = query.Where(u => u.Floor != null && u.Floor.Building != null && u.Floor.Building.ProjectId == projectId.Value);
            if (buildingId.HasValue) query = query.Where(u => u.Floor != null && u.Floor.Building != null && u.Floor.Building.Id == buildingId.Value);
            if (floorId.HasValue) query = query.Where(u => u.FloorId == floorId.Value);
            if (unitStatusId.HasValue) query = query.Where(u => u.UnitStatusId == unitStatusId.Value);
            if (unitTypeId.HasValue) query = query.Where(u => u.UnitTypeId == unitTypeId.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                query = query.Where(u => u.UnitNumber.Contains(s));
            }

            var total = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(u => u.UnitNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedData<Unit> { Items = items, TotalCount = total };
        }

        public async Task<Unit?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Units
                .AsNoTracking()
                .Include(u => u.Floor).ThenInclude(f => f.Building).ThenInclude(b => b.Project)
                .Include(u => u.UnitType)
                .Include(u => u.UnitStatus)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<List<Unit>> GetLookupAsync(int? projectId = null, int? unitStatusId = null)
        {
            var query = _context.Units.AsNoTracking().Include(u => u.UnitStatus).Include(u => u.Floor).ThenInclude(f => f.Building).ThenInclude(b => b.Project).Where(u => !u.IsDeleted);
            if (projectId.HasValue) query = query.Where(u => u.Floor != null && u.Floor.Building != null && u.Floor.Building.ProjectId == projectId.Value);
            if (unitStatusId.HasValue) query = query.Where(u => u.UnitStatusId == unitStatusId.Value);
            return await query.OrderBy(u => u.UnitNumber).ToListAsync();
        }
    }
}
