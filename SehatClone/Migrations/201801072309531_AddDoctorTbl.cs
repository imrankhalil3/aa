namespace SehatClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        Speciality = c.String(),
                        City = c.String(),
                        Gender = c.Int(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Doctors");
        }
    }
}
