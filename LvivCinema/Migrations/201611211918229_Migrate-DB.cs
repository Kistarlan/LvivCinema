namespace LvivCinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreFilm",
                c => new
                    {
                        GenreId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreId, t.FilmId })
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.FilmId);
            
            DropColumn("dbo.Films", "Genres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "Genres", c => c.String());
            DropForeignKey("dbo.GenreFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.GenreFilm", "GenreId", "dbo.Genres");
            DropIndex("dbo.GenreFilm", new[] { "FilmId" });
            DropIndex("dbo.GenreFilm", new[] { "GenreId" });
            DropTable("dbo.GenreFilm");
            DropTable("dbo.Genres");
        }
    }
}
