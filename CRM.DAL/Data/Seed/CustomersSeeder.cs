using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class CustomersSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            var customerTypes = new List<CustomerType>
            {
                new() { Name = "פרטי", Code = "PRIVATE", Description = "לקוח פרטי", IsActive = true },
                new() { Name = "חברה", Code = "COMPANY", Description = "חברת לקוחות", IsActive = true },
                new() { Name = "משקיע", Code = "INVESTOR", Description = "משקיע", IsActive = true },
                new() { Name = "תושב זר", Code = "FOREIGN_RESIDENT", Description = "תושב זר", IsActive = true },
                new() { Name = "אחר", Code = "OTHER", Description = "סוג לקוח אחר", IsActive = true }
            };

            foreach (var ct in customerTypes)
            {
                if (!await context.CustomerTypes.AnyAsync(x => x.Code == ct.Code))
                    await context.CustomerTypes.AddAsync(ct);
            }

            var customerStatuses = new List<CustomerStatus>
            {
                new() { Name = "פעיל", Code = "ACTIVE", Description = "לקוח פעיל", IsActive = true },
                new() { Name = "לא פעיל", Code = "INACTIVE", Description = "לא פעיל", IsActive = true },
                new() { Name = "פוטנציאלי", Code = "PROSPECT", Description = "לקוח פוטנציאלי", IsActive = true },
                new() { Name = "קונה", Code = "BUYER", Description = "קונה", IsActive = true },
                new() { Name = "לקוח לשעבר", Code = "PAST_CUSTOMER", Description = "לקוח בעבר", IsActive = true },
                new() { Name = "חסום", Code = "BLOCKED", Description = "חסום", IsActive = true }
            };

            foreach (var cs in customerStatuses)
            {
                if (!await context.CustomerStatuses.AnyAsync(x => x.Code == cs.Code))
                    await context.CustomerStatuses.AddAsync(cs);
            }

            await context.SaveChangesAsync();
        }
    }
}
