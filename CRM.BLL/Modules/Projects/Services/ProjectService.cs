using System;
using System.Collections.Generic;
using System.Text;

using CRM.BLL.Modules.Projects.DTOs;
using CRM.BLL.Modules.Projects.Interfaces;
using CRM.DAL.Modules.Projects.Repositories;
using CRM.DAL.Common.Repositories;
using CRM.DAL.Modules.Projects.Entities;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CRM.BLL.Modules.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProjectListDto>> GetActiveProjectsAsync()
        {
            var projects = await _projectRepository.GetActiveProjectsAsync();

            return projects.Select(MapToListDto).ToList();
        }

        public async Task<List<ProjectListDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return projects.Select(MapToListDto).ToList();
        }

        public async Task<ProjectDetailsDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdWithDetailsAsync(id);
            if (project == null) return null;
            return MapToDetailsDto(project);
        }

        public async Task<CRM.BLL.Common.ServiceResult<ProjectDetailsDto>> CreateAsync(CreateProjectDto dto)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(dto.Name))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("Name is required.");

            if (string.IsNullOrWhiteSpace(dto.Code))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("Code is required.");

            var code = dto.Code.Trim();

            if (await _projectRepository.CodeExistsAsync(code))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.Conflict("Code already exists.");

            if (!await _projectRepository.ProjectTypeExistsAsync(dto.ProjectTypeId))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectTypeId is invalid.");

            if (!await _projectRepository.ProjectStatusExistsAsync(dto.ProjectStatusId))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectStatusId is invalid.");

            if (dto.ProjectManagerUserId.HasValue)
            {
                if (!await _projectRepository.UserExistsAsync(dto.ProjectManagerUserId.Value))
                    return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectManagerUserId is invalid.");
            }

            if (dto.ConstructionStartDate.HasValue && dto.EstimatedCompletionDate.HasValue)
            {
                if (dto.EstimatedCompletionDate.Value < dto.ConstructionStartDate.Value)
                    return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("EstimatedCompletionDate cannot be before ConstructionStartDate.");
            }

            var project = new Project
            {
                Name = dto.Name.Trim(),
                Code = code,
                Description = dto.Description,
                ProjectTypeId = dto.ProjectTypeId,
                ProjectStatusId = dto.ProjectStatusId,
                City = dto.City,
                Street = dto.Street,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                MarketingStartDate = dto.MarketingStartDate,
                ConstructionStartDate = dto.ConstructionStartDate,
                EstimatedCompletionDate = dto.EstimatedCompletionDate,
                ActualCompletionDate = dto.ActualCompletionDate,
                LandArea = dto.LandArea,
                NumberOfBuildings = dto.NumberOfBuildings,
                NumberOfUnits = dto.NumberOfUnits,
                DeveloperName = dto.DeveloperName,
                ContractorName = dto.ContractorName,
                ProjectManagerUserId = dto.ProjectManagerUserId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            // Reload with details
            var created = await _projectRepository.GetByIdWithDetailsAsync(project.Id);
            return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.Success(created == null ? throw new InvalidOperationException("Created project not found") : MapToDetailsDto(created));
        }

        public async Task<CRM.BLL.Common.ServiceResult<ProjectDetailsDto>> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var project = await _projectRepository.GetByIdForUpdateAsync(id);
            if (project == null) return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.NotFound();

            if (project.IsDeleted) return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("Name is required.");

            if (string.IsNullOrWhiteSpace(dto.Code))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("Code is required.");

            var code = dto.Code.Trim();

            if (await _projectRepository.CodeExistsAsync(code, id))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.Conflict("Code already exists.");

            if (!await _projectRepository.ProjectTypeExistsAsync(dto.ProjectTypeId))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectTypeId is invalid.");

            if (!await _projectRepository.ProjectStatusExistsAsync(dto.ProjectStatusId))
                return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectStatusId is invalid.");

            if (dto.ProjectManagerUserId.HasValue)
            {
                if (!await _projectRepository.UserExistsAsync(dto.ProjectManagerUserId.Value))
                    return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("ProjectManagerUserId is invalid.");
            }

            if (dto.ConstructionStartDate.HasValue && dto.EstimatedCompletionDate.HasValue)
            {
                if (dto.EstimatedCompletionDate.Value < dto.ConstructionStartDate.Value)
                    return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.ValidationError("EstimatedCompletionDate cannot be before ConstructionStartDate.");
            }

            // Apply updates explicitly on tracked entity
            project.Name = dto.Name.Trim();
            project.Code = code;
            project.Description = dto.Description;
            project.ProjectTypeId = dto.ProjectTypeId;
            project.ProjectStatusId = dto.ProjectStatusId;
            project.City = dto.City;
            project.Street = dto.Street;
            project.Address = dto.Address;
            project.PostalCode = dto.PostalCode;
            project.Country = dto.Country;
            project.MarketingStartDate = dto.MarketingStartDate;
            project.ConstructionStartDate = dto.ConstructionStartDate;
            project.EstimatedCompletionDate = dto.EstimatedCompletionDate;
            project.ActualCompletionDate = dto.ActualCompletionDate;
            project.LandArea = dto.LandArea;
            project.NumberOfBuildings = dto.NumberOfBuildings;
            project.NumberOfUnits = dto.NumberOfUnits;
            project.DeveloperName = dto.DeveloperName;
            project.ContractorName = dto.ContractorName;
            project.ProjectManagerUserId = dto.ProjectManagerUserId;
            project.IsActive = dto.IsActive;
            project.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            var updated = await _projectRepository.GetByIdWithDetailsAsync(id);
            return CRM.BLL.Common.ServiceResult<ProjectDetailsDto>.Success(updated == null ? throw new InvalidOperationException("Updated project not found") : MapToDetailsDto(updated));
        }

        public async Task<CRM.BLL.Common.ServiceResult<object?>> DeleteAsync(int id)
        {
            var project = await _projectRepository.GetByIdForUpdateAsync(id);
            if (project == null) return CRM.BLL.Common.ServiceResult<object?>.NotFound();
            if (project.IsDeleted) return CRM.BLL.Common.ServiceResult<object?>.NotFound();

            project.IsDeleted = true;
            project.IsActive = false;
            project.UpdatedAt = DateTime.UtcNow;

            // Do not call repository.Update for tracked entity
            await _unitOfWork.SaveChangesAsync();

            return CRM.BLL.Common.ServiceResult<object?>.Success(null);
        }

        private ProjectListDto MapToListDto(Project project)
        {
            return new ProjectListDto
            {
                Id = project.Id,
                Name = project.Name,
                Code = project.Code,
                City = project.City ?? string.Empty,
                ProjectType = project.ProjectType?.Name ?? string.Empty,
                ProjectStatus = project.ProjectStatus?.Name ?? string.Empty,
                IsActive = project.IsActive
            };
        }

        private ProjectDetailsDto MapToDetailsDto(Project project)
        {
            return new ProjectDetailsDto
            {
                Id = project.Id,
                Name = project.Name,
                Code = project.Code,
                Description = project.Description,
                ProjectTypeId = project.ProjectTypeId,
                ProjectType = project.ProjectType?.Name ?? string.Empty,
                ProjectStatusId = project.ProjectStatusId,
                ProjectStatus = project.ProjectStatus?.Name ?? string.Empty,
                City = project.City,
                Street = project.Street,
                Address = project.Address,
                PostalCode = project.PostalCode,
                Country = project.Country,
                MarketingStartDate = project.MarketingStartDate,
                ConstructionStartDate = project.ConstructionStartDate,
                EstimatedCompletionDate = project.EstimatedCompletionDate,
                ActualCompletionDate = project.ActualCompletionDate,
                LandArea = project.LandArea,
                NumberOfBuildings = project.NumberOfBuildings,
                NumberOfUnits = project.NumberOfUnits,
                DeveloperName = project.DeveloperName,
                ContractorName = project.ContractorName,
                ProjectManagerUserId = project.ProjectManagerUserId,
                ProjectManagerName = project.ProjectManagerUser != null ?
                    $"{project.ProjectManagerUser.FirstName} {project.ProjectManagerUser.LastName}" : null,
                IsActive = project.IsActive,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }
    }
}
