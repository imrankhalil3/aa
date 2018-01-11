namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Description", c => c.String());
            AddColumn("dbo.Doctors", "Website", c => c.String());
            AddColumn("dbo.Doctors", "Education", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Education");
            DropColumn("dbo.Doctors", "Website");
            DropColumn("dbo.Doctors", "Description");
        }
    }
}
