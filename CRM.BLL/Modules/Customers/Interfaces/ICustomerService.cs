using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Common;
using System.Collections.Generic;

namespace CRM.BLL.Modules.Customers.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerListDto>> GetPagedAsync(CustomerFilter filter, PageRequest pageRequest);

        Task<CustomerDetailsDto?> GetByIdAsync(int id);

        Task<ServiceResult<CustomerDetailsDto>> CreateAsync(CreateCustomerDto dto);

        Task<ServiceResult<CustomerDetailsDto>> UpdateAsync(int id, UpdateCustomerDto dto);

        Task<ServiceResult<object>> DeleteAsync(int id);

        Task<List<CustomerContactDto>> GetContactsAsync(int customerId);

        Task<ServiceResult<CustomerContactDto>> LinkContactAsync(int customerId, LinkContactRequest request);

        Task<ServiceResult<object>> UnlinkContactAsync(int customerId, int contactId);
    }
}
