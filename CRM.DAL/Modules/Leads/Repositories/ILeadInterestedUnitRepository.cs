using System.Threading.Tasks;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public interface ILeadInterestedUnitRepository : IRepository<LeadInterestedUnit>
    {
        Task<LeadInterestedUnit?> GetByLeadAndUnitIncludingDeletedAsync(int leadId, int unitId);
    }
}
