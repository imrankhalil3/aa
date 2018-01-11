namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApprovedColInCenterAndDoctorTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Centers", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Doctors", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Approved");
            DropColumn("dbo.Centers", "Approved");
        }
    }
}
