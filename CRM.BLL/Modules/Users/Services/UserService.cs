using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.BLL.Modules.Users.DTOs;
using CRM.BLL.Modules.Users.Interfaces;
using CRM.DAL.Modules.Identity.Repositories;
using CRM.BLL.Common;

namespace CRM.BLL.Modules.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<List<UserLookupDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllActiveAsync();
            return users.Select(u => new UserLookupDto
            {
                Id = u.Id,
                DisplayName = string.IsNullOrWhiteSpace(u.FirstName) && string.IsNullOrWhiteSpace(u.LastName) ? u.Email : ($"{u.FirstName} {u.LastName}").Trim(),
                Email = u.Email,
                IsActive = u.IsActive
            }).ToList();
        }

        public async Task<ServiceResult<UserLookupDto>> GetByIdAsync(int id)
        {
            var u = await _userRepo.GetByIdAsync(id);
            if (u == null) return ServiceResult<UserLookupDto>.NotFound();

            var dto = new UserLookupDto
            {
                Id = u.Id,
                DisplayName = string.IsNullOrWhiteSpace(u.FirstName) && string.IsNullOrWhiteSpace(u.LastName) ? u.Email : ($"{u.FirstName} {u.LastName}").Trim(),
                Email = u.Email,
                IsActive = u.IsActive
            };

            return ServiceResult<UserLookupDto>.Success(dto);
        }
    }
}
