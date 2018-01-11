namespace SehatClone.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsApprovedCenter2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Centers", "IsApproved");
            AddColumn("dbo.Centers", "IsApproved", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Centers", "IsApproved");
        }
    }
}
