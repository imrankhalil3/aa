namespace SehatClone.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedArticleCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ArticleCategories (Name) VALUES ('Diet & Fitness'), ('Health Living'), ('Beauty & Relationship'), ('Pregnancy')");
        }

        public override void Down()
        {
            Sql("DELERE FROM ArticleCategories WHERE Name IN ('Diet & Fitness', 'Health Living', 'Beauty & Relationship', 'Pregnancy')");
        }
    }
}
