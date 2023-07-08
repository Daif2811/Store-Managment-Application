namespace StoreManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditProfileViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        CurrentPassword = c.String(nullable: false, maxLength: 100),
                        NewPassword = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserType");
            DropTable("dbo.EditProfileViewModels");
        }
    }
}
