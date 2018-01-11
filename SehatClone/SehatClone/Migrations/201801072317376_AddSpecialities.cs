namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecialities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "Speciality_Id", c => c.Int());
            CreateIndex("dbo.Doctors", "Speciality_Id");
            AddForeignKey("dbo.Doctors", "Speciality_Id", "dbo.Specialities", "Id");
            DropColumn("dbo.Doctors", "Speciality");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Speciality", c => c.String());
            DropForeignKey("dbo.Doctors", "Speciality_Id", "dbo.Specialities");
            DropIndex("dbo.Doctors", new[] { "Speciality_Id" });
            DropColumn("dbo.Doctors", "Speciality_Id");
            DropTable("dbo.Specialities");
        }
    }
}
