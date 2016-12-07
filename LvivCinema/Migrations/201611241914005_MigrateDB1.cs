namespace LvivCinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActorFilm",
                c => new
                    {
                        ActorId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActorId, t.FilmId })
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .Index(t => t.ActorId)
                .Index(t => t.FilmId);
            
            DropColumn("dbo.Films", "Actors");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Actors", c => c.String());
            DropForeignKey("dbo.ActorFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.ActorFilm", "ActorId", "dbo.Actors");
            DropIndex("dbo.ActorFilm", new[] { "FilmId" });
            DropIndex("dbo.ActorFilm", new[] { "ActorId" });
            DropTable("dbo.ActorFilm");
            DropTable("dbo.Actors");
        }
    }
}
