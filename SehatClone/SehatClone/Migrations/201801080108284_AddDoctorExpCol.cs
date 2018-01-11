namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorExpCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "ExperienceInYears", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "ExperienceInYears");
        }
    }
}
