namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescInCenterTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Centers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Centers", "Description");
        }
    }
}
