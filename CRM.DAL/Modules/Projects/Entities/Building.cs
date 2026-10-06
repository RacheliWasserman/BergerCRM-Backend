using System;
using System.Collections.Generic;
using System.Text;

using CRM.DAL.Common;

namespace CRM.DAL.Modules.Projects.Entities
{
    public class Building : BaseEntity
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;


        public int BuildingStatusId { get; set; }

        public BuildingStatus BuildingStatus { get; set; } = null!;


        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }


        public string? Entrance { get; set; }

        public string? Address { get; set; }


        public int? NumberOfFloors { get; set; }

        public int? NumberOfUnits { get; set; }


        public DateTime? ConstructionStartDate { get; set; }

        public DateTime? EstimatedCompletionDate { get; set; }

        public DateTime? ActualCompletionDate { get; set; }


        public bool IsActive { get; set; } = true;


        public ICollection<Floor> Floors { get; set; }
            = new List<Floor>();
    }
}
