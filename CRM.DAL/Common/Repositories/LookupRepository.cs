using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Models;
using CRM.DAL.Data;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Leads.Entities;
using CRM.DAL.Modules.Activities.Entities;
using CRM.DAL.Modules.Tasks.Entities;
using CRM.DAL.Modules.Deals.Entities;

namespace CRM.DAL.Common.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly CrmDbContext _context;

        public LookupRepository(CrmDbContext context)
        {
            _context = context;
        }

        private static LookupData Map(int id, string code, string name)
        {
            return new LookupData { Id = id, Code = code, Name = name };
        }

        public async Task<IReadOnlyList<LookupData>> GetProjectTypesAsync()
        {
            return await _context.ProjectTypes
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetProjectStatusesAsync()
        {
            return await _context.ProjectStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetBuildingStatusesAsync()
        {
            return await _context.BuildingStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetUnitTypesAsync()
        {
            return await _context.UnitTypes
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetUnitStatusesAsync()
        {
            return await _context.UnitStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetAssetTypesAsync()
        {
            return await _context.AssetTypes
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetAssetStatusesAsync()
        {
            return await _context.AssetStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetCustomerTypesAsync()
        {
            return await _context.CustomerTypes
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetCustomerStatusesAsync()
        {
            return await _context.CustomerStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetLeadSourcesAsync()
        {
            return await _context.LeadSources
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetLeadStatusesAsync()
        {
            return await _context.LeadStatuses
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetLostReasonsAsync()
        {
            return await _context.LostReasons
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetActivityTypesAsync()
        {
            return await _context.ActivityTypes
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetTaskStatesAsync()
        {
            // TaskState does not have SortOrder property; order by Name
            return await _context.TaskStates
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.Name)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetPrioritiesAsync()
        {
            return await _context.Priorities
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.SortOrder)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<LookupData>> GetDealStagesAsync()
        {
            return await _context.DealStages
                .Where(x => !x.IsDeleted && x.IsActive)
                .OrderBy(x => x.SortOrder)
                .Select(x => new LookupData { Id = x.Id, Code = x.Code, Name = x.Name })
                .ToListAsync();
        }
    }
}
