namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePerson : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Person", newName: "Instructor");
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Instructor", "HireDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Instructor", "EnrollmentDate");
            DropColumn("dbo.Instructor", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructor", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Instructor", "EnrollmentDate", c => c.DateTime());
            AlterColumn("dbo.Instructor", "HireDate", c => c.DateTime());
            DropTable("dbo.Student");
            RenameTable(name: "dbo.Instructor", newName: "Person");
        }
    }
}
