using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public class LeadPreferenceRepository : Repository<LeadPreference>, ILeadPreferenceRepository
    {
        public LeadPreferenceRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<LeadPreference?> GetByLeadIdIncludingDeletedAsync(int leadId)
        {
            return await _context.LeadPreferences.FirstOrDefaultAsync(p => p.LeadId == leadId);
        }
    }
}
