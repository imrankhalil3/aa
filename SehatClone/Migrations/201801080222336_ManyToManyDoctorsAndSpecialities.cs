namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyDoctorsAndSpecialities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Speciality_Id", "dbo.Specialities");
            DropIndex("dbo.Doctors", new[] { "Speciality_Id" });
            CreateTable(
                "dbo.SpecialityDoctors",
                c => new
                    {
                        Speciality_Id = c.Int(nullable: false),
                        Doctor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Speciality_Id, t.Doctor_Id })
                .ForeignKey("dbo.Specialities", t => t.Speciality_Id, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id, cascadeDelete: true)
                .Index(t => t.Speciality_Id)
                .Index(t => t.Doctor_Id);
            
            DropColumn("dbo.Doctors", "Speciality_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Speciality_Id", c => c.Int());
            DropForeignKey("dbo.SpecialityDoctors", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.SpecialityDoctors", "Speciality_Id", "dbo.Specialities");
            DropIndex("dbo.SpecialityDoctors", new[] { "Doctor_Id" });
            DropIndex("dbo.SpecialityDoctors", new[] { "Speciality_Id" });
            DropTable("dbo.SpecialityDoctors");
            CreateIndex("dbo.Doctors", "Speciality_Id");
            AddForeignKey("dbo.Doctors", "Speciality_Id", "dbo.Specialities", "Id");
        }
    }
}
