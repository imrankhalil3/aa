namespace SehatClone.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles VALUES (1, 'admin')");
        }

        public override void Down()
        {
            Sql("DELETE FROM AspNetRoles WHERE Id = 1");
        }
    }
}
