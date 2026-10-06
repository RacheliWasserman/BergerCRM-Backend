using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.BLL.Modules.Projects.DTOs
{
    public class ProjectListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string ProjectType { get; set; } = string.Empty;

        public string ProjectStatus { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}