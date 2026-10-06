using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.DAL.Data;
using CRM.DAL.Modules.Projects.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM.DAL.Data.Seed
{
    public static class ProjectsSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            // ProjectTypes
            var projectTypes = new List<ProjectType>
            {
                new() { Name = "מגורים", Code = "RESIDENTIAL", Description = "פרויקט למגורים", IsActive = true },
                new() { Name = "מסחרי", Code = "COMMERCIAL", Description = "פרויקט מסחרי", IsActive = true },
                new() { Name = "שימוש מעורב", Code = "MIXED_USE", Description = "פרויקט שימוש מעורב", IsActive = true },
                new() { Name = "חידוש עירוני", Code = "URBAN_RENEWAL", Description = "פרויקט חידוש עירוני", IsActive = true }
            };

            foreach (var pt in projectTypes)
            {
                if (!await context.ProjectTypes.AnyAsync(x => x.Code == pt.Code))
                    await context.ProjectTypes.AddAsync(pt);
            }

            // ProjectStatuses
            var projectStatuses = new List<ProjectStatus>
            {
                new() { Name = "תכנון", Code = "PLANNING", Description = "בשלבי תכנון", IsActive = true },
                new() { Name = "טרום שיווק", Code = "PRE_MARKETING", Description = "טרום שיווק", IsActive = true },
                new() { Name = "שיווק", Code = "MARKETING", Description = "בשלבי שיווק", IsActive = true },
                new() { Name = "בבנייה", Code = "UNDER_CONSTRUCTION", Description = "בניה שוטפת", IsActive = true },
                new() { Name = "מוכן", Code = "READY", Description = "מוכן למגורים/מסחר", IsActive = true },
                new() { Name = "הושלם", Code = "COMPLETED", Description = "פרויקט הושלם", IsActive = true },
                new() { Name = "בהשהיה", Code = "ON_HOLD", Description = "הפרויקט בהשהיה", IsActive = true },
                new() { Name = "מבוטל", Code = "CANCELLED", Description = "הפרויקט בוטל", IsActive = true }
            };

            foreach (var ps in projectStatuses)
            {
                if (!await context.ProjectStatuses.AnyAsync(x => x.Code == ps.Code))
                    await context.ProjectStatuses.AddAsync(ps);
            }

            // BuildingStatuses
            var buildingStatuses = new List<BuildingStatus>
            {
                new() { Name = "מתוכנן", Code = "PLANNED", Description = "בניין מתוכנן", IsActive = true },
                new() { Name = "בבנייה", Code = "UNDER_CONSTRUCTION", Description = "בבנייה", IsActive = true },
                new() { Name = "מוכן", Code = "READY", Description = "מוכן", IsActive = true },
                new() { Name = "הושלם", Code = "COMPLETED", Description = "הושלם", IsActive = true },
                new() { Name = "בהשהיה", Code = "ON_HOLD", Description = "בהשהיה", IsActive = true }
            };

            foreach (var bs in buildingStatuses)
            {
                if (!await context.BuildingStatuses.AnyAsync(x => x.Code == bs.Code))
                    await context.BuildingStatuses.AddAsync(bs);
            }

            // UnitTypes
            var unitTypes = new List<UnitType>
            {
                new() { Name = "דירה 3 חדרים", Code = "APARTMENT_3_ROOMS", Description = "דירת 3 חדרים", DefaultRooms = 3, IsActive = true },
                new() { Name = "דירה 4 חדרים", Code = "APARTMENT_4_ROOMS", Description = "דירת 4 חדרים", DefaultRooms = 4, IsActive = true },
                new() { Name = "דירה 5 חדרים", Code = "APARTMENT_5_ROOMS", Description = "דירת 5 חדרים", DefaultRooms = 5, IsActive = true },
                new() { Name = "פנטהאוז", Code = "PENTHOUSE", Description = "פנטהאוז", DefaultRooms = null, IsActive = true },
                new() { Name = "דירת גן", Code = "GARDEN_APARTMENT", Description = "דירת גן", DefaultRooms = null, IsActive = true },
                new() { Name = "דופלקס", Code = "DUPLEX", Description = "דירה דו-מפלסית / דופלקס", DefaultRooms = null, IsActive = true },
                new() { Name = "מסחרי", Code = "COMMERCIAL", Description = "שטח מסחרי", DefaultRooms = null, IsActive = true },
                new() { Name = "משרדים", Code = "OFFICE", Description = "חלל משרדי", DefaultRooms = null, IsActive = true }
            };

            foreach (var ut in unitTypes)
            {
                if (!await context.UnitTypes.AnyAsync(x => x.Code == ut.Code))
                    await context.UnitTypes.AddAsync(ut);
            }

            // UnitStatuses
            var unitStatuses = new List<UnitStatus>
            {
                new() { Name = "זמין", Code = "AVAILABLE", Description = "זמין למכירה", IsActive = true },
                new() { Name = "שמור", Code = "RESERVED", Description = "היחידה שמורה", IsActive = true },
                new() { Name = "במשא ומתן", Code = "UNDER_NEGOTIATION", Description = "היחידה נמצאת בתהליך משא ומתן", IsActive = true },
                new() { Name = "נמכר", Code = "SOLD", Description = "נמכר", IsActive = true },
                new() { Name = "חסום", Code = "BLOCKED", Description = "חסום", IsActive = true },
                new() { Name = "לא למכירה", Code = "NOT_FOR_SALE", Description = "לא מיועד למכירה", IsActive = true },
                new() { Name = "מבוטל", Code = "CANCELLED", Description = "העסקה בוטלה", IsActive = true }
            };

            foreach (var us in unitStatuses)
            {
                if (!await context.UnitStatuses.AnyAsync(x => x.Code == us.Code))
                    await context.UnitStatuses.AddAsync(us);
            }

            // AssetTypes
            var assetTypes = new List<AssetType>
            {
                new() { Name = "חניה", Code = "PARKING", Description = "חניה" , IsActive = true },
                new() { Name = "מחסן", Code = "STORAGE", Description = "מחסן", IsActive = true },
                new() { Name = "גג", Code = "ROOF", Description = "שטח גג", IsActive = true },
                new() { Name = "גינה", Code = "GARDEN", Description = "גינה פרטית", IsActive = true },
                new() { Name = "סיומת מסחרית", Code = "COMMERCIAL_ATTACHMENT", Description = "תוספת מסחרית", IsActive = true },
                new() { Name = "אחר", Code = "OTHER", Description = "אחר", IsActive = true }
            };

            foreach (var at in assetTypes)
            {
                if (!await context.AssetTypes.AnyAsync(x => x.Code == at.Code))
                    await context.AssetTypes.AddAsync(at);
            }

            // AssetStatuses
            var assetStatuses = new List<AssetStatus>
            {
                new() { Name = "זמין", Code = "AVAILABLE", Description = "זמין", IsActive = true },
                new() { Name = "שמור", Code = "RESERVED", Description = "שמור", IsActive = true },
                new() { Name = "שויך", Code = "ASSIGNED", Description = "שויך ללקוח/יחידה", IsActive = true },
                new() { Name = "נמכר", Code = "SOLD", Description = "נמכר", IsActive = true },
                new() { Name = "חסום", Code = "BLOCKED", Description = "חסום", IsActive = true },
                new() { Name = "לא זמין", Code = "NOT_AVAILABLE", Description = "לא זמין", IsActive = true }
            };

            foreach (var ast in assetStatuses)
            {
                if (!await context.AssetStatuses.AnyAsync(x => x.Code == ast.Code))
                    await context.AssetStatuses.AddAsync(ast);
            }

            await context.SaveChangesAsync();
        }
    }
}
