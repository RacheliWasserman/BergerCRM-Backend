using System;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Common;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Modules.Customers.Interfaces;
using CRM.DAL.Modules.Customers.Repositories;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Common.Repositories;

namespace CRM.BLL.Modules.Customers.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository contactRepo, IUnitOfWork unitOfWork)
        {
            _contactRepo = contactRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ContactListDto>> GetPagedAsync(ContactFilter filter, PageRequest pageRequest)
        {
            var data = await _contactRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.IsActive, filter.Search);

            var items = data.Items.Select(c => new ContactListDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                City = c.City,
                IsActive = c.IsActive
            }).ToList();

            return new PagedResult<ContactListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<ContactDetailsDto?> GetByIdAsync(int id)
        {
            var c = await _contactRepo.GetByIdForUpdateAsync(id);
            if (c == null) return null;

            return new ContactDetailsDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                SecondaryPhone = c.SecondaryPhone,
                IdentificationNumber = c.IdentificationNumber,
                BirthDate = c.BirthDate,
                Address = c.Address,
                City = c.City,
                PostalCode = c.PostalCode,
                Country = c.Country,
                PreferredLanguage = c.PreferredLanguage,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt
            };
        }

        public async Task<ServiceResult<ContactDetailsDto>> CreateAsync(CreateContactDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return ServiceResult<ContactDetailsDto>.ValidationError("FirstName is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                return ServiceResult<ContactDetailsDto>.ValidationError("LastName is required.");

            if (dto.FirstName.Trim().Length > 100 || dto.LastName.Trim().Length > 100)
                return ServiceResult<ContactDetailsDto>.ValidationError("FirstName/LastName max length is 100.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email.Trim().Length > 256)
                return ServiceResult<ContactDetailsDto>.ValidationError("Email max length is 256.");

            var contact = new Contact
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim(),
                Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim(),
                SecondaryPhone = string.IsNullOrWhiteSpace(dto.SecondaryPhone) ? null : dto.SecondaryPhone.Trim(),
                IdentificationNumber = string.IsNullOrWhiteSpace(dto.IdentificationNumber) ? null : dto.IdentificationNumber.Trim(),
                BirthDate = dto.BirthDate,
                Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim(),
                City = string.IsNullOrWhiteSpace(dto.City) ? null : dto.City.Trim(),
                PostalCode = string.IsNullOrWhiteSpace(dto.PostalCode) ? null : dto.PostalCode.Trim(),
                Country = string.IsNullOrWhiteSpace(dto.Country) ? null : dto.Country.Trim(),
                PreferredLanguage = string.IsNullOrWhiteSpace(dto.PreferredLanguage) ? null : dto.PreferredLanguage.Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _contactRepo.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync();

            var created = await _contactRepo.GetByIdForUpdateAsync(contact.Id);

            var result = new ContactDetailsDto
            {
                Id = created!.Id,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                Phone = created.Phone,
                SecondaryPhone = created.SecondaryPhone,
                IdentificationNumber = created.IdentificationNumber,
                BirthDate = created.BirthDate,
                Address = created.Address,
                City = created.City,
                PostalCode = created.PostalCode,
                Country = created.Country,
                PreferredLanguage = created.PreferredLanguage,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt
            };

            return ServiceResult<ContactDetailsDto>.Success(result);
        }

        public async Task<ServiceResult<ContactDetailsDto>> UpdateAsync(int id, UpdateContactDto dto)
        {
            var contact = await _contactRepo.GetByIdForUpdateAsync(id);
            if (contact == null || contact.IsDeleted)
                return ServiceResult<ContactDetailsDto>.NotFound();

            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                return ServiceResult<ContactDetailsDto>.ValidationError("FirstName and LastName are required.");

            if (dto.FirstName.Trim().Length > 100 || dto.LastName.Trim().Length > 100)
                return ServiceResult<ContactDetailsDto>.ValidationError("FirstName/LastName max length is 100.");

            contact.FirstName = dto.FirstName.Trim();
            contact.LastName = dto.LastName.Trim();
            contact.Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim();
            contact.Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim();
            contact.SecondaryPhone = string.IsNullOrWhiteSpace(dto.SecondaryPhone) ? null : dto.SecondaryPhone.Trim();
            contact.IdentificationNumber = string.IsNullOrWhiteSpace(dto.IdentificationNumber) ? null : dto.IdentificationNumber.Trim();
            contact.BirthDate = dto.BirthDate;
            contact.Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim();
            contact.City = string.IsNullOrWhiteSpace(dto.City) ? null : dto.City.Trim();
            contact.PostalCode = string.IsNullOrWhiteSpace(dto.PostalCode) ? null : dto.PostalCode.Trim();
            contact.Country = string.IsNullOrWhiteSpace(dto.Country) ? null : dto.Country.Trim();
            contact.PreferredLanguage = string.IsNullOrWhiteSpace(dto.PreferredLanguage) ? null : dto.PreferredLanguage.Trim();
            contact.IsActive = dto.IsActive;
            contact.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            var updated = await _contactRepo.GetByIdForUpdateAsync(contact.Id);

            var result = new ContactDetailsDto
            {
                Id = updated!.Id,
                FirstName = updated.FirstName,
                LastName = updated.LastName,
                Email = updated.Email,
                Phone = updated.Phone,
                SecondaryPhone = updated.SecondaryPhone,
                IdentificationNumber = updated.IdentificationNumber,
                BirthDate = updated.BirthDate,
                Address = updated.Address,
                City = updated.City,
                PostalCode = updated.PostalCode,
                Country = updated.Country,
                PreferredLanguage = updated.PreferredLanguage,
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt
            };

            return ServiceResult<ContactDetailsDto>.Success(result);
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            var contact = await _contactRepo.GetByIdForUpdateAsync(id);
            if (contact == null || contact.IsDeleted)
                return ServiceResult<object>.NotFound();

            contact.IsDeleted = true;
            contact.IsActive = false;
            contact.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<object>.Success(new { });
        }
    }
}
