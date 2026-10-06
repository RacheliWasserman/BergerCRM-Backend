namespace CRM.BLL.Modules.Tasks.DTOs
{
    public class TaskFilter
    {
        public int? TaskStateId { get; set; }
        public int? PriorityId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? CreatedForLeadId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
    }
}
