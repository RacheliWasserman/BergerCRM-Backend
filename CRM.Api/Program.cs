using CRM.DAL.Data;
using Microsoft.EntityFrameworkCore;
using CRM.BLL.Modules.Projects.Interfaces;
using CRM.BLL.Modules.Projects.Services;
using CRM.DAL.Modules.Projects.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Entity Framework Core + SQL Server
builder.Services.AddDbContext<CrmDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<CRM.DAL.Common.Repositories.IUnitOfWork, CRM.DAL.Common.Repositories.UnitOfWork>();
builder.Services.AddScoped<CRM.DAL.Common.Repositories.ILookupRepository, CRM.DAL.Common.Repositories.LookupRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Common.Interfaces.ILookupService, CRM.BLL.Modules.Common.Services.LookupService>();
// Activities
builder.Services.AddScoped<CRM.DAL.Modules.Activities.Repositories.IActivityRepository, CRM.DAL.Modules.Activities.Repositories.ActivityRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Activities.Interfaces.IActivityService, CRM.BLL.Modules.Activities.Services.ActivityService>();
// Tasks
builder.Services.AddScoped<CRM.DAL.Modules.Tasks.Repositories.ITaskRepository, CRM.DAL.Modules.Tasks.Repositories.TaskRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Tasks.Interfaces.ITaskService, CRM.BLL.Modules.Tasks.Services.TaskService>();
// Customers module registrations
builder.Services.AddScoped<CRM.DAL.Modules.Customers.Repositories.ICustomerRepository, CRM.DAL.Modules.Customers.Repositories.CustomerRepository>();
builder.Services.AddScoped<CRM.DAL.Modules.Customers.Repositories.IContactRepository, CRM.DAL.Modules.Customers.Repositories.ContactRepository>();
builder.Services.AddScoped<CRM.DAL.Modules.Customers.Repositories.ICustomerContactRepository, CRM.DAL.Modules.Customers.Repositories.CustomerContactRepository>();

builder.Services.AddScoped<CRM.BLL.Modules.Customers.Interfaces.ICustomerService, CRM.BLL.Modules.Customers.Services.CustomerService>();
builder.Services.AddScoped<CRM.BLL.Modules.Customers.Interfaces.IContactService, CRM.BLL.Modules.Customers.Services.ContactService>();

// Leads module registrations
builder.Services.AddScoped<CRM.DAL.Modules.Leads.Repositories.ILeadRepository, CRM.DAL.Modules.Leads.Repositories.LeadRepository>();
builder.Services.AddScoped<CRM.DAL.Modules.Leads.Repositories.ILeadPreferenceRepository, CRM.DAL.Modules.Leads.Repositories.LeadPreferenceRepository>();
builder.Services.AddScoped<CRM.DAL.Modules.Leads.Repositories.ILeadInterestedUnitRepository, CRM.DAL.Modules.Leads.Repositories.LeadInterestedUnitRepository>();
builder.Services.AddScoped<CRM.DAL.Modules.Leads.Repositories.ILeadAssignmentRepository, CRM.DAL.Modules.Leads.Repositories.LeadAssignmentRepository>();

builder.Services.AddScoped<CRM.BLL.Modules.Leads.Interfaces.ILeadService, CRM.BLL.Modules.Leads.Services.LeadService>();

// Deals
builder.Services.AddScoped<CRM.DAL.Modules.Deals.Repositories.IDealRepository, CRM.DAL.Modules.Deals.Repositories.DealRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Deals.Interfaces.IDealService, CRM.BLL.Modules.Deals.Services.DealService>();

// Audit
builder.Services.AddScoped<CRM.DAL.Modules.Audit.Repositories.IAuditLogRepository, CRM.DAL.Modules.Audit.Repositories.AuditLogRepository>();
builder.Services.AddScoped<CRM.BLL.Audit.IAuditService, CRM.BLL.Audit.AuditService>();

// Users
builder.Services.AddScoped<CRM.DAL.Modules.Identity.Repositories.IUserRepository, CRM.DAL.Modules.Identity.Repositories.UserRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Users.Interfaces.IUserService, CRM.BLL.Modules.Users.Services.UserService>();

// Units
builder.Services.AddScoped<CRM.DAL.Modules.Projects.Repositories.IUnitRepository, CRM.DAL.Modules.Projects.Repositories.UnitRepository>();
builder.Services.AddScoped<CRM.BLL.Modules.Units.Interfaces.IUnitService, CRM.BLL.Modules.Units.Services.UnitService>();

// Swagger/OpenAPI (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();