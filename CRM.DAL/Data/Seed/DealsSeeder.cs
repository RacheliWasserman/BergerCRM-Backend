using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Deals.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class DealsSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            var stages = new List<DealStage>
            {
                new() { Name = "חדש", Code = "NEW", Description = "עסקה חדשה", SortOrder = 1, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "אימות", Code = "QUALIFICATION", Description = "שלב האימות", SortOrder = 2, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "בחירת יחידה", Code = "UNIT_SELECTED", Description = "נבחרה יחידה", SortOrder = 3, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "הצעה", Code = "OFFER", Description = "הצעת מחיר", SortOrder = 4, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "משא ומתן", Code = "NEGOTIATION", Description = "משא ומתן", SortOrder = 5, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "הזמנת שמירה", Code = "RESERVATION", Description = "שלב שמירה/הזמנה", SortOrder = 6, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "חוזה", Code = "CONTRACT", Description = "חתימת חוזה", SortOrder = 7, IsClosed = false, IsWon = false, IsActive = true },
                new() { Name = "עסקה נסגרה", Code = "WON", Description = "העסקה נסגרה בהצלחה", SortOrder = 8, IsClosed = true, IsWon = true, IsActive = true },
                new() { Name = "עסקה שלא נסגרה", Code = "LOST", Description = "העסקה נסגרה ללא מכירה", SortOrder = 9, IsClosed = true, IsWon = false, IsActive = true }
            };

            foreach (var ds in stages)
            {
                if (!await context.DealStages.AnyAsync(x => x.Code == ds.Code))
                    await context.DealStages.AddAsync(ds);
            }

            await context.SaveChangesAsync();
        }
    }
}
