using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public class LeadInterestedUnitRepository : Repository<LeadInterestedUnit>, ILeadInterestedUnitRepository
    {
        public LeadInterestedUnitRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<LeadInterestedUnit?> GetByLeadAndUnitIncludingDeletedAsync(int leadId, int unitId)
        {
            return await _context.LeadInterestedUnits.FirstOrDefaultAsync(lu => lu.LeadId == leadId && lu.UnitId == unitId);
        }
    }
}
