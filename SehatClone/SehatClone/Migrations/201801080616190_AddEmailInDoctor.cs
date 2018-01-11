namespace SehatClone.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddEmailInDoctor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "Email");
            AddColumn("dbo.Doctors", "Email", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Doctors", "Email");
        }
    }
}
