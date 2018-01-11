namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetailInCenterTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CenterServicesCenters",
                c => new
                    {
                        CenterServices_Id = c.Int(nullable: false),
                        Center_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CenterServices_Id, t.Center_Id })
                .ForeignKey("dbo.CenterServices", t => t.CenterServices_Id, cascadeDelete: true)
                .ForeignKey("dbo.Centers", t => t.Center_Id, cascadeDelete: true)
                .Index(t => t.CenterServices_Id)
                .Index(t => t.Center_Id);
            
            AddColumn("dbo.Centers", "ImageUrl", c => c.String());
            AddColumn("dbo.Centers", "ForGender", c => c.String());
            AddColumn("dbo.Centers", "Timings", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CenterServicesCenters", "Center_Id", "dbo.Centers");
            DropForeignKey("dbo.CenterServicesCenters", "CenterServices_Id", "dbo.CenterServices");
            DropIndex("dbo.CenterServicesCenters", new[] { "Center_Id" });
            DropIndex("dbo.CenterServicesCenters", new[] { "CenterServices_Id" });
            DropColumn("dbo.Centers", "Timings");
            DropColumn("dbo.Centers", "ForGender");
            DropColumn("dbo.Centers", "ImageUrl");
            DropTable("dbo.CenterServicesCenters");
        }
    }
}
