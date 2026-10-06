using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Data;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public class LeadAssignmentRepository : Repository<LeadAssignment>, ILeadAssignmentRepository
    {
        public LeadAssignmentRepository(CrmDbContext context)
            : base(context)
        {
        }

        public async Task<List<LeadAssignment>> GetActiveAssignmentsForLeadAsync(int leadId)
        {
            return await _context.LeadAssignments
                .Where(a => a.LeadId == leadId && a.IsActive && !a.IsDeleted)
                .ToListAsync();
        }
    }
}
