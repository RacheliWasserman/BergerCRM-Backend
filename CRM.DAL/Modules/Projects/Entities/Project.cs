using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Modules.Identity.Entities;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public int ProjectTypeId { get; set; }

        public ProjectType ProjectType { get; set; } = null!;

        public int ProjectStatusId { get; set; }

        public ProjectStatus ProjectStatus { get; set; } = null!;


        // Location

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }


        // Dates

        public DateTime? MarketingStartDate { get; set; }

        public DateTime? ConstructionStartDate { get; set; }

        public DateTime? EstimatedCompletionDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }


        // General information

        public decimal? LandArea { get; set; }

        public int? NumberOfBuildings { get; set; }

        public int? NumberOfUnits { get; set; }

        public string? DeveloperName { get; set; }

        public string? ContractorName { get; set; }


        // Contact / project management

        public int? ProjectManagerUserId { get; set; }
        public User? ProjectManagerUser { get; set; }
        public bool IsActive { get; set; } = true;


        // Navigation

        public ICollection<ProjectUser> ProjectUsers { get; set; }
            = new List<ProjectUser>();

        public ICollection<Building> Buildings { get; set; }
            = new List<Building>();

        public ICollection<ProjectAsset> ProjectAssets { get; set; }
            = new List<ProjectAsset>();
    }
}
