using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Customers.Entities;

namespace CRM.DAL.Modules.Customers.Repositories
{
    public interface ICustomerContactRepository : IRepository<CustomerContact>
    {
        Task<List<CustomerContact>> GetActiveLinksForCustomerAsync(int customerId);

        Task<CustomerContact?> FindAnyLinkAsync(int customerId, int contactId);

        Task<CustomerContact?> GetActiveLinkForCustomerContactAsync(int customerId, int contactId);

        Task<CustomerContact?> GetActivePrimaryForCustomerAsync(int customerId);
    }
}
