using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Leads.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class LeadsSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            var sources = new List<LeadSource>
            {
                new() { Name = "אתר", Code = "WEBSITE", Description = "נוצר מהאתר", IsActive = true },
                new() { Name = "טלפון", Code = "PHONE", Description = "פניות טלפוניות", IsActive = true },
                new() { Name = "פייסבוק", Code = "FACEBOOK", Description = "מקור: Facebook", IsActive = true },
                new() { Name = "אינסטגרם", Code = "INSTAGRAM", Description = "מקור: Instagram", IsActive = true },
                new() { Name = "גוגל", Code = "GOOGLE", Description = "מקור: Google", IsActive = true },
                new() { Name = "קמפיין", Code = "CAMPAIGN", Description = "קמפיין שיווקי", IsActive = true },
                new() { Name = "מתווך", Code = "BROKER", Description = "פניות מצד מתווך", IsActive = true },
                new() { Name = "המלצה", Code = "REFERRAL", Description = "הפנייה/המלצה", IsActive = true },
                new() { Name = "הגעה למשרד המכירות", Code = "WALK_IN", Description = "פנייה בעקבות הגעה למשרד המכירות", IsActive = true },
                new() { Name = "אחר", Code = "OTHER", Description = "מקור אחר", IsActive = true }
            };

            foreach (var s in sources)
            {
                if (!await context.LeadSources.AnyAsync(x => x.Code == s.Code))
                    await context.LeadSources.AddAsync(s);
            }

            var statuses = new List<LeadStatus>
            {
                new() { Name = "חדש", Code = "NEW", Description = "ליד חדש", IsActive = true },
                new() { Name = "יצירת קשר", Code = "CONTACTED", Description = "נוצר קשר", IsActive = true },
                new() { Name = "ליד מוסמך", Code = "QUALIFIED", Description = "ליד שעבר תהליך סינון ונמצא מתאים", IsActive = true },
                new() { Name = "פגישת תיאום", Code = "MEETING_SCHEDULED", Description = "פגישה מתוזמנת", IsActive = true },
                new() { Name = "פגישה בוצעה", Code = "MEETING_COMPLETED", Description = "פגישה בוצעה", IsActive = true },
                new() { Name = "משא ומתן", Code = "NEGOTIATION", Description = "בתהליך משא ומתן", IsActive = true },
                new() { Name = "הומר", Code = "CONVERTED", Description = "הומר לעסקה/לקוח", IsActive = true },
                new() { Name = "אבוד", Code = "LOST", Description = "אבד", IsActive = true },
                new() { Name = "בהשהיה", Code = "ON_HOLD", Description = "בהשהיה", IsActive = true }
            };

            foreach (var st in statuses)
            {
                if (!await context.LeadStatuses.AnyAsync(x => x.Code == st.Code))
                    await context.LeadStatuses.AddAsync(st);
            }

            var lostReasons = new List<LostReason>
            {
                new() { Name = "מחיר", Code = "PRICE", Description = "מחיר גבוה", IsActive = true },
                new() { Name = "אין מענה", Code = "NO_RESPONSE", Description = "אין מענה מהלקוח", IsActive = true },
                new() { Name = "לא רלוונטי", Code = "NOT_RELEVANT", Description = "לא רלוונטי", IsActive = true },
                new() { Name = "נרכש במקום אחר", Code = "BOUGHT_ELSEWHERE", Description = "נרכש במקום אחר", IsActive = true },
                new() { Name = "מימון", Code = "FINANCING", Description = "בעיות במימון", IsActive = true },
                new() { Name = "תזמון", Code = "TIMING", Description = "תזמון לא מתאים", IsActive = true },
                new() { Name = "הפרויקט לא מתאים", Code = "PROJECT_NOT_SUITABLE", Description = "הפרויקט לא מתאים", IsActive = true },
                new() { Name = "אחר", Code = "OTHER", Description = "סיבה אחרת", IsActive = true }
            };

            foreach (var lr in lostReasons)
            {
                if (!await context.LostReasons.AnyAsync(x => x.Code == lr.Code))
                    await context.LostReasons.AddAsync(lr);
            }

            await context.SaveChangesAsync();
        }
    }
}
