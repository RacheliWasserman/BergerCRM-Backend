using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public interface ILeadAssignmentRepository : IRepository<LeadAssignment>
    {
        Task<List<LeadAssignment>> GetActiveAssignmentsForLeadAsync(int leadId);
    }
}
