using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Tasks.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class TasksSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            var states = new List<TaskState>
            {
                new() { Name = "פתוח", Code = "OPEN", Description = "פתוח", IsActive = true },
                new() { Name = "בעבודה", Code = "IN_PROGRESS", Description = "בעבודה", IsActive = true },
                new() { Name = "הושלם", Code = "COMPLETED", Description = "הושלם", IsActive = true },
                new() { Name = "מבוטל", Code = "CANCELLED", Description = "מבוטל", IsActive = true },
                new() { Name = "באיחור", Code = "OVERDUE", Description = "באיחור", IsActive = true }
            };

            foreach (var s in states)
            {
                if (!await context.TaskStates.AnyAsync(x => x.Code == s.Code))
                    await context.TaskStates.AddAsync(s);
            }

            var priorities = new List<Priority>
            {
                new() { Name = "נמוך", Code = "LOW", Description = "עדיפות נמוכה", SortOrder = 1, IsActive = true },
                new() { Name = "רגיל", Code = "NORMAL", Description = "עדיפות רגילה", SortOrder = 2, IsActive = true },
                new() { Name = "גבוה", Code = "HIGH", Description = "עדיפות גבוהה", SortOrder = 3, IsActive = true },
                new() { Name = "דחוף", Code = "URGENT", Description = "עדיפות דחופה", SortOrder = 4, IsActive = true }
            };

            foreach (var p in priorities)
            {
                if (!await context.Priorities.AnyAsync(x => x.Code == p.Code))
                    await context.Priorities.AddAsync(p);
            }

            await context.SaveChangesAsync();
        }
    }
}
