using System;

namespace CRM.BLL.Modules.Projects.DTOs
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int ProjectTypeId { get; set; }

        public string ProjectType { get; set; } = string.Empty;

        public int ProjectStatusId { get; set; }

        public string ProjectStatus { get; set; } = string.Empty;

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public DateTime? MarketingStartDate { get; set; }

        public DateTime? ConstructionStartDate { get; set; }

        public DateTime? EstimatedCompletionDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }

        public decimal? LandArea { get; set; }

        public int? NumberOfBuildings { get; set; }

        public int? NumberOfUnits { get; set; }

        public string? DeveloperName { get; set; }

        public string? ContractorName { get; set; }

        public int? ProjectManagerUserId { get; set; }

        public string? ProjectManagerName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
