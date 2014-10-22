using System.Data.Entity;

namespace ContosoUniversity.DAL
{
    public class CreateDatabaseInitializer : CreateDatabaseIfNotExists<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            SeedData.Create(context); 
            base.Seed(context);
        }
    }

    public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            SeedData.Create(context);
            base.Seed(context);
        }
    } 
}