namespace SehatClone.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserToAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetUserRoles values ('476eac01-2414-4b91-b86b-11d5d5437a64', 1)");
        }

        public override void Down()
        {
            Sql("Delete From AspNetUserRoles WHERE UserId = '476eac01-2414-4b91-b86b-11d5d5437a64' and RoleId = 1");

        }
    }
}