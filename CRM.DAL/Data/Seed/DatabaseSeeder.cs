using System;
using System.Collections.Generic;
using System.Text;
using CRM.DAL.Data;
namespace CRM.DAL.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(CrmDbContext context)
        {
            // 1. Identity (roles & permissions)
            await IdentitySeeder.SeedAsync(context);

            // 2. Projects lookups
            await ProjectsSeeder.SeedAsync(context);

            // 3. Customers lookups
            await CustomersSeeder.SeedAsync(context);

            // 4. Leads lookups
            await LeadsSeeder.SeedAsync(context);

            // 5. Activities lookups
            await ActivitiesSeeder.SeedAsync(context);

            // 6. Tasks lookups
            await TasksSeeder.SeedAsync(context);

            // 7. Deals lookups
            await DealsSeeder.SeedAsync(context);
        }
    }
}
