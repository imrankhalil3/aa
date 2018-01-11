namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsApprovedCenter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Centers", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Centers", "IsApproved");
        }
    }
}
