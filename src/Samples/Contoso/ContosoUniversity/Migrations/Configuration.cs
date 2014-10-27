using ContosoUniversity.DAL;
using System.Data.Entity.Migrations;

namespace ContosoUniversity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SchoolContext context)
        {
            SeedData.Create(context);
        }
    }
}