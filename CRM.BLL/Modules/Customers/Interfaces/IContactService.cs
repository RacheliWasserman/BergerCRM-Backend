using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Common;
using System.Collections.Generic;

namespace CRM.BLL.Modules.Customers.Interfaces
{
    public interface IContactService
    {
        Task<PagedResult<ContactListDto>> GetPagedAsync(ContactFilter filter, PageRequest pageRequest);

        Task<ContactDetailsDto?> GetByIdAsync(int id);

        Task<ServiceResult<ContactDetailsDto>> CreateAsync(CreateContactDto dto);

        Task<ServiceResult<ContactDetailsDto>> UpdateAsync(int id, UpdateContactDto dto);

        Task<ServiceResult<object>> DeleteAsync(int id);
    }
}
