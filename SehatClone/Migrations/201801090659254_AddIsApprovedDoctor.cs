namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsApprovedDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "IsApproved");
        }
    }
}
