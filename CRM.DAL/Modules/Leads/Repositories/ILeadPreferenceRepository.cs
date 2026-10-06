using System.Threading.Tasks;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Leads.Entities;

namespace CRM.DAL.Modules.Leads.Repositories
{
    public interface ILeadPreferenceRepository : IRepository<LeadPreference>
    {
        Task<LeadPreference?> GetByLeadIdIncludingDeletedAsync(int leadId);
    }
}
