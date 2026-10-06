namespace CRM.BLL.Modules.Units.DTOs
{
    public class UnitLookupDto
    {
        public int Id { get; set; }
        public string Display { get; set; } = null!;
        public int ProjectId { get; set; }
        public int UnitStatusId { get; set; }
        public string? UnitStatusName { get; set; }
    }
}
