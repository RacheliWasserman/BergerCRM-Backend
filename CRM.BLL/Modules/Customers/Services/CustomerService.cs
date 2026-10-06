using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Common;
using CRM.BLL.Modules.Customers.DTOs;
using CRM.BLL.Modules.Customers.Interfaces;
using CRM.DAL.Modules.Customers.Repositories;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Common.Models;
using CRM.DAL.Common.Repositories;

namespace CRM.BLL.Modules.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IContactRepository _contactRepo;
        private readonly ICustomerContactRepository _customerContactRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(
            ICustomerRepository customerRepo,
            IContactRepository contactRepo,
            ICustomerContactRepository customerContactRepo,
            IUnitOfWork unitOfWork)
        {
            _customerRepo = customerRepo;
            _contactRepo = contactRepo;
            _customerContactRepo = customerContactRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<CustomerListDto>> GetPagedAsync(CustomerFilter filter, PageRequest pageRequest)
        {
            var data = await _customerRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, filter.CustomerTypeId, filter.CustomerStatusId, filter.IsActive, filter.Search);

            var items = data.Items.Select(c => new CustomerListDto
            {
                Id = c.Id,
                CustomerNumber = c.CustomerNumber,
                DisplayName = c.DisplayName,
                CustomerTypeId = c.CustomerTypeId,
                CustomerTypeName = c.CustomerType?.Name ?? string.Empty,
                CustomerStatusId = c.CustomerStatusId,
                CustomerStatusName = c.CustomerStatus?.Name ?? string.Empty,
                CustomerSince = c.CustomerSince,
                IsActive = c.IsActive
            }).ToList();

            return new PagedResult<CustomerListDto>
            {
                Items = items,
                TotalCount = data.TotalCount,
                Page = pageRequest.Page,
                PageSize = pageRequest.PageSize
            };
        }

        public async Task<CustomerDetailsDto?> GetByIdAsync(int id)
        {
            var c = await _customerRepo.GetByIdWithDetailsAsync(id);
            if (c == null) return null;

            return new CustomerDetailsDto
            {
                Id = c.Id,
                CustomerNumber = c.CustomerNumber,
                DisplayName = c.DisplayName,
                CustomerTypeId = c.CustomerTypeId,
                CustomerTypeName = c.CustomerType?.Name ?? string.Empty,
                CustomerStatusId = c.CustomerStatusId,
                CustomerStatusName = c.CustomerStatus?.Name ?? string.Empty,
                CustomerSince = c.CustomerSince,
                Notes = c.Notes,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt
            };
        }

        public async Task<ServiceResult<CustomerDetailsDto>> CreateAsync(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerNumber))
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerNumber is required.");

            var number = dto.CustomerNumber.Trim();
            if (number.Length > 50)
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerNumber max length is 50.");

            if (await _customerRepo.CustomerNumberExistsAsync(number))
                return ServiceResult<CustomerDetailsDto>.Conflict("CustomerNumber already exists.");

            if (!await _customerRepo.CustomerTypeExistsAsync(dto.CustomerTypeId))
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerTypeId is invalid.");

            if (!await _customerRepo.CustomerStatusExistsAsync(dto.CustomerStatusId))
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerStatusId is invalid.");

            var customer = new Customer
            {
                CustomerNumber = number,
                CustomerTypeId = dto.CustomerTypeId,
                CustomerStatusId = dto.CustomerStatusId,
                DisplayName = string.IsNullOrWhiteSpace(dto.DisplayName) ? null : dto.DisplayName.Trim(),
                CustomerSince = dto.CustomerSince ?? DateTime.UtcNow,
                Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _customerRepo.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            var created = await _customerRepo.GetByIdWithDetailsAsync(customer.Id);
            var resultDto = new CustomerDetailsDto
            {
                Id = created!.Id,
                CustomerNumber = created.CustomerNumber,
                DisplayName = created.DisplayName,
                CustomerTypeId = created.CustomerTypeId,
                CustomerTypeName = created.CustomerType?.Name ?? string.Empty,
                CustomerStatusId = created.CustomerStatusId,
                CustomerStatusName = created.CustomerStatus?.Name ?? string.Empty,
                CustomerSince = created.CustomerSince,
                Notes = created.Notes,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt
            };

            return ServiceResult<CustomerDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<CustomerDetailsDto>> UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepo.GetByIdForUpdateAsync(id);
            if (customer == null || customer.IsDeleted)
                return ServiceResult<CustomerDetailsDto>.NotFound();

            if (!await _customerRepo.CustomerTypeExistsAsync(dto.CustomerTypeId))
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerTypeId is invalid.");

            if (!await _customerRepo.CustomerStatusExistsAsync(dto.CustomerStatusId))
                return ServiceResult<CustomerDetailsDto>.ValidationError("CustomerStatusId is invalid.");

            if (!string.IsNullOrWhiteSpace(dto.DisplayName) && dto.DisplayName.Length > 200)
                return ServiceResult<CustomerDetailsDto>.ValidationError("DisplayName max length is 200.");

            if (!string.IsNullOrWhiteSpace(dto.Notes) && dto.Notes.Length > 2000)
                return ServiceResult<CustomerDetailsDto>.ValidationError("Notes max length is 2000.");

            // Apply updates
            customer.DisplayName = string.IsNullOrWhiteSpace(dto.DisplayName) ? null : dto.DisplayName.Trim();
            customer.CustomerTypeId = dto.CustomerTypeId;
            customer.CustomerStatusId = dto.CustomerStatusId;
            customer.Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim();
            customer.IsActive = dto.IsActive;
            customer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            var updated = await _customerRepo.GetByIdWithDetailsAsync(customer.Id);
            var resultDto = new CustomerDetailsDto
            {
                Id = updated!.Id,
                CustomerNumber = updated.CustomerNumber,
                DisplayName = updated.DisplayName,
                CustomerTypeId = updated.CustomerTypeId,
                CustomerTypeName = updated.CustomerType?.Name ?? string.Empty,
                CustomerStatusId = updated.CustomerStatusId,
                CustomerStatusName = updated.CustomerStatus?.Name ?? string.Empty,
                CustomerSince = updated.CustomerSince,
                Notes = updated.Notes,
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt
            };

            return ServiceResult<CustomerDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<object>> DeleteAsync(int id)
        {
            var customer = await _customerRepo.GetByIdForUpdateAsync(id);
            if (customer == null || customer.IsDeleted)
                return ServiceResult<object>.NotFound();

            customer.IsDeleted = true;
            customer.IsActive = false;
            customer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<object>.Success(new { });
        }

        public async Task<List<CustomerContactDto>> GetContactsAsync(int customerId)
        {
            var customer = await _customerRepo.GetByIdWithDetailsAsync(customerId);
            if (customer == null || customer.IsDeleted) return null!;

            var links = await _customerContactRepo.GetActiveLinksForCustomerAsync(customerId);

            return links.Select(l => new CustomerContactDto
            {
                ContactId = l.ContactId,
                FirstName = l.Contact.FirstName,
                LastName = l.Contact.LastName,
                Email = l.Contact.Email,
                Phone = l.Contact.Phone,
                RelationshipType = l.RelationshipType,
                IsPrimary = l.IsPrimary,
                IsActive = l.IsActive,
                LinkedAt = l.LinkedAt
            }).ToList();
        }

        public async Task<ServiceResult<CustomerContactDto>> LinkContactAsync(int customerId, LinkContactRequest request)
        {
            var customer = await _customerRepo.GetByIdForUpdateAsync(customerId);
            if (customer == null || customer.IsDeleted)
                return ServiceResult<CustomerContactDto>.NotFound("Customer not found.");

            var contact = await _contactRepo.GetByIdForUpdateAsync(request.ContactId);
            if (contact == null || contact.IsDeleted)
                return ServiceResult<CustomerContactDto>.NotFound("Contact not found.");

            string? rel = string.IsNullOrWhiteSpace(request.RelationshipType) ? null : request.RelationshipType.Trim();
            if (rel != null && rel.Length > 100)
                return ServiceResult<CustomerContactDto>.ValidationError("RelationshipType max length is 100.");

            var existingAny = await _customerContactRepo.FindAnyLinkAsync(customerId, request.ContactId);
            if (existingAny != null && !existingAny.IsDeleted && existingAny.IsActive)
                return ServiceResult<CustomerContactDto>.Conflict("Contact already linked to customer.");

            // Handle primary switching
            CustomerContact? existingPrimary = null;
            if (request.IsPrimary)
            {
                existingPrimary = await _customerContactRepo.GetActivePrimaryForCustomerAsync(customerId);
                if (existingPrimary != null && existingPrimary.ContactId == request.ContactId)
                {
                    // already primary on same link
                    // continue to ensure reactivation
                }
            }

            if (existingAny != null)
            {
                // Reactivate
                if (existingAny.IsDeleted || !existingAny.IsActive)
                {
                    // if making primary, unset other primary
                    if (request.IsPrimary && existingPrimary != null && existingPrimary.Id != existingAny.Id)
                    {
                        existingPrimary.IsPrimary = false;
                        existingPrimary.UpdatedAt = DateTime.UtcNow;
                    }

                    existingAny.IsDeleted = false;
                    existingAny.IsActive = true;
                    existingAny.UnlinkedAt = null;
                    existingAny.LinkedAt = DateTime.UtcNow;
                    existingAny.RelationshipType = rel;
                    existingAny.IsPrimary = request.IsPrimary;
                    existingAny.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.SaveChangesAsync();

                    var dto = new CustomerContactDto
                    {
                        ContactId = existingAny.ContactId,
                        FirstName = existingAny.Contact.FirstName,
                        LastName = existingAny.Contact.LastName,
                        Email = existingAny.Contact.Email,
                        Phone = existingAny.Contact.Phone,
                        RelationshipType = existingAny.RelationshipType,
                        IsPrimary = existingAny.IsPrimary,
                        IsActive = existingAny.IsActive,
                        LinkedAt = existingAny.LinkedAt
                    };

                    return ServiceResult<CustomerContactDto>.Success(dto);
                }
            }

            // Create new
            if (request.IsPrimary && existingPrimary != null)
            {
                existingPrimary.IsPrimary = false;
                existingPrimary.UpdatedAt = DateTime.UtcNow;
            }

            var link = new CustomerContact
            {
                CustomerId = customerId,
                ContactId = request.ContactId,
                RelationshipType = rel,
                IsPrimary = request.IsPrimary,
                IsActive = true,
                LinkedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            await _customerContactRepo.AddAsync(link);
            await _unitOfWork.SaveChangesAsync();

            // Include contact info - reload
            var created = await _customerContactRepo.FindAnyLinkAsync(customerId, request.ContactId);

            var result = new CustomerContactDto
            {
                ContactId = created!.ContactId,
                FirstName = created.Contact.FirstName,
                LastName = created.Contact.LastName,
                Email = created.Contact.Email,
                Phone = created.Contact.Phone,
                RelationshipType = created.RelationshipType,
                IsPrimary = created.IsPrimary,
                IsActive = created.IsActive,
                LinkedAt = created.LinkedAt
            };

            return ServiceResult<CustomerContactDto>.Success(result);
        }

        public async Task<ServiceResult<object>> UnlinkContactAsync(int customerId, int contactId)
        {
            var customer = await _customerRepo.GetByIdForUpdateAsync(customerId);
            if (customer == null || customer.IsDeleted)
                return ServiceResult<object>.NotFound();

            var link = await _customerContactRepo.GetActiveLinkForCustomerContactAsync(customerId, contactId);
            if (link == null)
                return ServiceResult<object>.NotFound();

            link.IsPrimary = false;
            link.IsActive = false;
            link.IsDeleted = true;
            link.UnlinkedAt = DateTime.UtcNow;
            link.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<object>.Success(new { });
        }
    }
}
