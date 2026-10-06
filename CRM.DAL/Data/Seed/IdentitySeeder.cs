using CRM.DAL.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            // =========================
            // Roles
            // =========================
            if (!await context.Roles.AnyAsync())
            {
                var rolesToAdd = new List<Role>
                {
                    new()
                    {
                        Name = "מנהל מערכת",
                        Code = "ADMIN",
                        Description = "מנהל עסקי בעל הרשאות ניהול רחבות במערכת",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "הנהלה",
                        Code = "MANAGEMENT",
                        Description = "משתמש הנהלה",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל מכירות",
                        Code = "SALES_MANAGER",
                        Description = "מנהל צוות המכירות",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "נציג מכירות",
                        Code = "SALES_AGENT",
                        Description = "משתמש בצוות המכירות",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל שיווק",
                        Code = "MARKETING_MANAGER",
                        Description = "מנהל תחום השיווק",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "משתמש שיווק",
                        Code = "MARKETING_USER",
                        Description = "משתמש בצוות השיווק",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל כספים",
                        Code = "FINANCE_MANAGER",
                        Description = "מנהל תחום הכספים",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "משתמש כספים",
                        Code = "FINANCE_USER",
                        Description = "משתמש בצוות הכספים",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל פרויקט",
                        Code = "PROJECT_MANAGER",
                        Description = "מנהל פרויקט",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "משתמש פרויקט",
                        Code = "PROJECT_USER",
                        Description = "משתמש בצוות הפרויקט",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל שירות לקוחות",
                        Code = "CUSTOMER_SERVICE_MANAGER",
                        Description = "מנהל תחום שירות הלקוחות",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "נציג שירות לקוחות",
                        Code = "CUSTOMER_SERVICE_USER",
                        Description = "משתמש בצוות שירות הלקוחות",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מחלקה משפטית",
                        Code = "LEGAL",
                        Description = "משתמש בתחום המשפטי והחוזים",
                        IsSystemRole = true,
                        IsActive = true
                    },
                    new()
                    {
                        Name = "מנהל טכני",
                        Code = "SYSTEM_ADMIN",
                        Description = "מנהל טכני של המערכת",
                        IsSystemRole = true,
                        IsActive = true
                    }
                };

                await context.Roles.AddRangeAsync(rolesToAdd);
                await context.SaveChangesAsync();
            }

            // =========================
            // Permissions
            // =========================
            if (!await context.Permissions.AnyAsync())
            {
                var permissions = new List<Permission>
                {
                    // Users
                    new() { Code = "USERS.VIEW", Name = "צפייה במשתמשים", Module = "Users", IsActive = true },
                    new() { Code = "USERS.CREATE", Name = "יצירת משתמשים", Module = "Users", IsActive = true },
                    new() { Code = "USERS.UPDATE", Name = "עדכון משתמשים", Module = "Users", IsActive = true },
                    new() { Code = "USERS.DELETE", Name = "מחיקת משתמשים", Module = "Users", IsActive = true },

                    // Roles & Permissions
                    new() { Code = "ROLES.VIEW", Name = "צפייה בתפקידים והרשאות", Module = "Roles", IsActive = true },
                    new() { Code = "ROLES.MANAGE", Name = "ניהול תפקידים והרשאות", Module = "Roles", IsActive = true },

                    // Projects
                    new() { Code = "PROJECTS.VIEW", Name = "צפייה בפרויקטים", Module = "Projects", IsActive = true },
                    new() { Code = "PROJECTS.CREATE", Name = "יצירת פרויקטים", Module = "Projects", IsActive = true },
                    new() { Code = "PROJECTS.UPDATE", Name = "עדכון פרויקטים", Module = "Projects", IsActive = true },
                    new() { Code = "PROJECTS.DELETE", Name = "מחיקת פרויקטים", Module = "Projects", IsActive = true },

                    // Units
                    new() { Code = "UNITS.VIEW", Name = "צפייה ביחידות", Module = "Units", IsActive = true },
                    new() { Code = "UNITS.CREATE", Name = "יצירת יחידות", Module = "Units", IsActive = true },
                    new() { Code = "UNITS.UPDATE", Name = "עדכון יחידות", Module = "Units", IsActive = true },
                    new() { Code = "UNITS.CHANGE_STATUS", Name = "שינוי סטטוס יחידה", Module = "Units", IsActive = true },
                    new() { Code = "UNITS.CHANGE_PRICE", Name = "שינוי מחיר יחידה", Module = "Units", IsActive = true },

                    // Customers
                    new() { Code = "CUSTOMERS.VIEW", Name = "צפייה בלקוחות", Module = "Customers", IsActive = true },
                    new() { Code = "CUSTOMERS.CREATE", Name = "יצירת לקוחות", Module = "Customers", IsActive = true },
                    new() { Code = "CUSTOMERS.UPDATE", Name = "עדכון לקוחות", Module = "Customers", IsActive = true },
                    new() { Code = "CUSTOMERS.DELETE", Name = "מחיקת לקוחות", Module = "Customers", IsActive = true },

                    // Leads
                    new() { Code = "LEADS.VIEW", Name = "צפייה בלידים", Module = "Leads", IsActive = true },
                    new() { Code = "LEADS.CREATE", Name = "יצירת לידים", Module = "Leads", IsActive = true },
                    new() { Code = "LEADS.UPDATE", Name = "עדכון לידים", Module = "Leads", IsActive = true },
                    new() { Code = "LEADS.DELETE", Name = "מחיקת לידים", Module = "Leads", IsActive = true },
                    new() { Code = "LEADS.ASSIGN", Name = "שיוך לידים למשתמשים", Module = "Leads", IsActive = true },
                    new() { Code = "LEADS.CONVERT", Name = "המרת ליד", Module = "Leads", IsActive = true },

                    // Activities
                    new() { Code = "ACTIVITIES.VIEW", Name = "צפייה בפעילויות", Module = "Activities", IsActive = true },
                    new() { Code = "ACTIVITIES.CREATE", Name = "יצירת פעילויות", Module = "Activities", IsActive = true },
                    new() { Code = "ACTIVITIES.UPDATE", Name = "עדכון פעילויות", Module = "Activities", IsActive = true },

                    // Tasks
                    new() { Code = "TASKS.VIEW", Name = "צפייה במשימות", Module = "Tasks", IsActive = true },
                    new() { Code = "TASKS.CREATE", Name = "יצירת משימות", Module = "Tasks", IsActive = true },
                    new() { Code = "TASKS.UPDATE", Name = "עדכון משימות", Module = "Tasks", IsActive = true },
                    new() { Code = "TASKS.DELETE", Name = "מחיקת משימות", Module = "Tasks", IsActive = true },
                    new() { Code = "TASKS.ASSIGN", Name = "שיוך משימות", Module = "Tasks", IsActive = true },

                    // Deals
                    new() { Code = "DEALS.VIEW", Name = "צפייה בעסקאות", Module = "Deals", IsActive = true },
                    new() { Code = "DEALS.CREATE", Name = "יצירת עסקאות", Module = "Deals", IsActive = true },
                    new() { Code = "DEALS.UPDATE", Name = "עדכון עסקאות", Module = "Deals", IsActive = true },
                    new() { Code = "DEALS.DELETE", Name = "מחיקת עסקאות", Module = "Deals", IsActive = true },
                    new() { Code = "DEALS.CHANGE_STAGE", Name = "שינוי שלב עסקה", Module = "Deals", IsActive = true },

                    // Audit
                    new() { Code = "AUDIT.VIEW", Name = "צפייה ביומן ביקורת", Module = "Audit", IsActive = true }
                };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }

            // =========================
            // RolePermissions (V1 matrix)
            // =========================
            // Build and validate roles and permissions, then add missing RolePermission records.

            // Roles involved in the matrix
            var requiredRoleCodes = new[]
            {
                "ADMIN",
                "SYSTEM_ADMIN",
                "MANAGEMENT",
                "SALES_MANAGER",
                "SALES_AGENT",
                "MARKETING_MANAGER",
                "MARKETING_USER",
                "FINANCE_MANAGER",
                "FINANCE_USER",
                "PROJECT_MANAGER",
                "PROJECT_USER",
                "CUSTOMER_SERVICE_MANAGER",
                "CUSTOMER_SERVICE_USER",
                "LEGAL"
            };

            var dbRoles = await context.Roles.Where(r => requiredRoleCodes.Contains(r.Code)).ToListAsync();
            var missingRoles = requiredRoleCodes.Except(dbRoles.Select(r => r.Code)).ToList();
            if (missingRoles.Any())
                throw new InvalidOperationException($"Missing required Role Codes for RolePermissions seeding: {string.Join(",", missingRoles)}");

            // load active permissions
            var allPermissions = await context.Permissions.Where(p => p.IsActive).ToListAsync();
            var permByCode = allPermissions.ToDictionary(p => p.Code, StringComparer.OrdinalIgnoreCase);

            // (validation done below) 

            // Define matrix exactly as specified (Codes only)
            var matrix = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
            {
                // ADMIN and SYSTEM_ADMIN will be assigned all permissions below
                { "ADMIN", Enumerable.Empty<string>() },
                { "SYSTEM_ADMIN", Enumerable.Empty<string>() },

                { "MANAGEMENT", new[] {
                    "USERS.VIEW",
                    "ROLES.VIEW",
                    "PROJECTS.VIEW",
                    "UNITS.VIEW",
                    "CUSTOMERS.VIEW",
                    "LEADS.VIEW",
                    "ACTIVITIES.VIEW",
                    "TASKS.VIEW",
                    "DEALS.VIEW",
                    "AUDIT.VIEW" } },

                { "SALES_MANAGER", new[] {
                    "PROJECTS.VIEW",
                    "UNITS.VIEW","UNITS.UPDATE","UNITS.CHANGE_STATUS","UNITS.CHANGE_PRICE",
                    "CUSTOMERS.VIEW","CUSTOMERS.CREATE","CUSTOMERS.UPDATE",
                    "LEADS.VIEW","LEADS.CREATE","LEADS.UPDATE","LEADS.ASSIGN","LEADS.CONVERT",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE","ACTIVITIES.UPDATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE","TASKS.ASSIGN",
                    "DEALS.VIEW","DEALS.CREATE","DEALS.UPDATE","DEALS.CHANGE_STAGE" } },

                { "SALES_AGENT", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW","CUSTOMERS.CREATE","CUSTOMERS.UPDATE",
                    "LEADS.VIEW","LEADS.CREATE","LEADS.UPDATE","LEADS.CONVERT",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE","ACTIVITIES.UPDATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE",
                    "DEALS.VIEW","DEALS.CREATE","DEALS.UPDATE" } },

                { "MARKETING_MANAGER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW",
                    "LEADS.VIEW","LEADS.CREATE","LEADS.UPDATE",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE",
                    "DEALS.VIEW" } },

                { "MARKETING_USER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "LEADS.VIEW","LEADS.CREATE",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE" } },

                { "FINANCE_MANAGER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW",
                    "LEADS.VIEW",
                    "DEALS.VIEW",
                    "ACTIVITIES.VIEW",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE" } },

                { "FINANCE_USER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW","CUSTOMERS.VIEW","DEALS.VIEW","TASKS.VIEW" } },

                { "PROJECT_MANAGER", new[] {
                    "PROJECTS.VIEW","PROJECTS.UPDATE",
                    "UNITS.VIEW","UNITS.CREATE","UNITS.UPDATE","UNITS.CHANGE_STATUS",
                    "CUSTOMERS.VIEW",
                    "LEADS.VIEW",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE","TASKS.ASSIGN",
                    "DEALS.VIEW" } },

                { "PROJECT_USER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW","LEADS.VIEW",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE",
                    "DEALS.VIEW" } },

                { "CUSTOMER_SERVICE_MANAGER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW","CUSTOMERS.CREATE","CUSTOMERS.UPDATE",
                    "LEADS.VIEW",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE","ACTIVITIES.UPDATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE","TASKS.ASSIGN",
                    "DEALS.VIEW" } },

                { "CUSTOMER_SERVICE_USER", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW","CUSTOMERS.UPDATE",
                    "LEADS.VIEW",
                    "ACTIVITIES.VIEW","ACTIVITIES.CREATE",
                    "TASKS.VIEW","TASKS.CREATE","TASKS.UPDATE",
                    "DEALS.VIEW" } },

                { "LEGAL", new[] {
                    "PROJECTS.VIEW","UNITS.VIEW",
                    "CUSTOMERS.VIEW",
                    "LEADS.VIEW",
                    "ACTIVITIES.VIEW",
                    "TASKS.VIEW",
                    "DEALS.VIEW" } }
            };

            // Resolve ADMIN and SYSTEM_ADMIN to all available permissions
            var allPermCodes = allPermissions.Select(p => p.Code).ToList();
            matrix["ADMIN"] = allPermCodes;
            matrix["SYSTEM_ADMIN"] = allPermCodes;

            // Validate that all permission codes referenced in matrix exist
            var referencedPermCodes = matrix.Where(kv => kv.Value != null).SelectMany(kv => kv.Value).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var missingPerms = referencedPermCodes.Where(c => !allPermissions.Any(p => string.Equals(p.Code, c, StringComparison.OrdinalIgnoreCase))).ToList();
            if (missingPerms.Any())
                throw new InvalidOperationException($"Missing required Permission Codes for RolePermissions seeding: {string.Join(",", missingPerms)}");

            // Load existing RolePermissions for roles of interest
            var roleIdMap = dbRoles.ToDictionary(r => r.Code, r => r.Id, StringComparer.OrdinalIgnoreCase);
            var roleIds = roleIdMap.Values.ToList();
            var existingRPs = await context.RolePermissions.Where(rp => roleIds.Contains(rp.RoleId)).Select(rp => new { rp.RoleId, rp.PermissionId }).ToListAsync();
            var existingSet = new HashSet<string>(existingRPs.Select(e => e.RoleId + ":" + e.PermissionId));

            var toAdd = new List<RolePermission>();

            foreach (var kv in matrix)
            {
                var roleCode = kv.Key;
                if (!roleIdMap.TryGetValue(roleCode, out var roleId))
                    continue; // already validated above

                foreach (var permCode in kv.Value)
                {
                    var perm = allPermissions.First(p => string.Equals(p.Code, permCode, StringComparison.OrdinalIgnoreCase));
                    var key = roleId + ":" + perm.Id;
                    if (!existingSet.Contains(key))
                    {
                        toAdd.Add(new RolePermission { RoleId = roleId, PermissionId = perm.Id });
                        existingSet.Add(key);
                    }
                }
            }

            if (toAdd.Any())
            {
                await context.RolePermissions.AddRangeAsync(toAdd);
                await context.SaveChangesAsync();
            }
        }
    }
}