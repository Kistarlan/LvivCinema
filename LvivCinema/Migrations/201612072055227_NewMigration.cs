namespace LvivCinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cinemas", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Cinemas", "Adress", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cinemas", "Adress", c => c.String());
            AlterColumn("dbo.Cinemas", "Name", c => c.String());
        }
    }
}
