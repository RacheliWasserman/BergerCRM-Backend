using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.BLL.Modules.Users.DTOs;

namespace CRM.BLL.Modules.Users.Interfaces
{
    public interface IUserService
    {
        Task<System.Collections.Generic.List<UserLookupDto>> GetAllAsync();

        Task<CRM.BLL.Common.ServiceResult<UserLookupDto>> GetByIdAsync(int id);
    }
}
