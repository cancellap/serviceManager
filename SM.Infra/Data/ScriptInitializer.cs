using SM.Domaiin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infra.Data
{
    public static class ScriptInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            CreateRoles(context);

        }
        private static void CreateRoles(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Nome = "Cliente", Id = 1 },
                    new Role { Nome = "Tecnico", Id = 2 },
                    new Role { Nome = "Manager", Id = 3 },
                    new Role { Nome = "Admin", Id = 4 }
                );
                context.SaveChanges();
            }
        }
    }
}
