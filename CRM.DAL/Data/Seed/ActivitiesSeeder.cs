using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Activities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class ActivitiesSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            var activityTypes = new List<ActivityType>
            {
                new() { Name = "שיחת טלפון", Code = "PHONE_CALL", Description = "שיחת טלפון", IsActive = true },
                new() { Name = "דוא״ל", Code = "EMAIL", Description = "אימייל", IsActive = true },
                new() { Name = "ווטסאפ", Code = "WHATSAPP", Description = "WhatsApp", IsActive = true },
                new() { Name = "SMS", Code = "SMS", Description = "הודעת SMS", IsActive = true },
                new() { Name = "פגישה", Code = "MEETING", Description = "פגישה", IsActive = true },
                new() { Name = "הערה", Code = "NOTE", Description = "הערה פנימית", IsActive = true },
                new() { Name = "שינוי סטטוס", Code = "STATUS_CHANGE", Description = "שינוי סטטוס", IsActive = true },
                new() { Name = "מערכת", Code = "SYSTEM", Description = "פעולה מערכתית", IsActive = true }
            };

            foreach (var at in activityTypes)
            {
                if (!await context.ActivityTypes.AnyAsync(x => x.Code == at.Code))
                    await context.ActivityTypes.AddAsync(at);
            }

            await context.SaveChangesAsync();
        }
    }
}
