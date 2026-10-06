using Microsoft.EntityFrameworkCore;

using CRM.DAL.Modules.Identity.Entities;
using CRM.DAL.Modules.Projects.Entities;
using CRM.DAL.Modules.Customers.Entities;
using CRM.DAL.Modules.Leads.Entities;
using CRM.DAL.Modules.Activities.Entities;
using CRM.DAL.Modules.Tasks.Entities;
using CRM.DAL.Modules.Deals.Entities;
using CRM.DAL.Modules.Audit.Entities;

namespace CRM.DAL.Data
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options)
            : base(options)
        {
        }

        // Identity
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
        public DbSet<LoginHistory> LoginHistories => Set<LoginHistory>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        // Projects
        public DbSet<ProjectType> ProjectTypes => Set<ProjectType>();
        public DbSet<ProjectStatus> ProjectStatuses => Set<ProjectStatus>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectUser> ProjectUsers => Set<ProjectUser>();
        public DbSet<BuildingStatus> BuildingStatuses => Set<BuildingStatus>();
        public DbSet<Building> Buildings => Set<Building>();
        public DbSet<Floor> Floors => Set<Floor>();
        public DbSet<UnitType> UnitTypes => Set<UnitType>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<UnitStatus> UnitStatuses => Set<UnitStatus>();
        public DbSet<UnitStatusHistory> UnitStatusHistory => Set<UnitStatusHistory>();
        public DbSet<UnitPriceHistory> UnitPriceHistories => Set<UnitPriceHistory>();
        public DbSet<AssetType> AssetTypes => Set<AssetType>();
        public DbSet<AssetStatus> AssetStatuses => Set<AssetStatus>();
        public DbSet<ProjectAsset> ProjectAssets => Set<ProjectAsset>();
        public DbSet<UnitAssetAssignment> UnitAssetAssignments => Set<UnitAssetAssignment>();

        // Customers
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();
        public DbSet<CustomerStatus> CustomerStatuses => Set<CustomerStatus>();
        public DbSet<CustomerContact> CustomerContacts => Set<CustomerContact>();

        // Leads
        public DbSet<Lead> Leads => Set<Lead>();
        public DbSet<LeadSource> LeadSources => Set<LeadSource>();
        public DbSet<LeadStatus> LeadStatuses => Set<LeadStatus>();
        public DbSet<LeadStatusHistory> LeadStatusHistories => Set<LeadStatusHistory>();
        public DbSet<LostReason> LostReasons => Set<LostReason>();
        public DbSet<LeadAssignment> LeadAssignments => Set<LeadAssignment>();
        public DbSet<LeadPreference> LeadPreferences => Set<LeadPreference>();
        public DbSet<LeadInterestedUnit> LeadInterestedUnits => Set<LeadInterestedUnit>();

        // Activities
        public DbSet<ActivityType> ActivityTypes => Set<ActivityType>();
        public DbSet<Activity> Activities => Set<Activity>();

        // Tasks
        public DbSet<TaskState> TaskStates => Set<TaskState>();
        public DbSet<Priority> Priorities => Set<Priority>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        // Deals
        public DbSet<DealStage> DealStages => Set<DealStage>();
        public DbSet<Deal> Deals => Set<Deal>();
        public DbSet<DealStageHistory> DealStageHistory => Set<DealStageHistory>();

        // Audit
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrmDbContext).Assembly);
        }
    }
}
