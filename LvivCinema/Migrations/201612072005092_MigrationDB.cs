namespace LvivCinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        NumberSeats = c.Int(nullable: false),
                        CinemaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId)
                .Index(t => t.CinemaId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dataTime = c.DateTime(nullable: false),
                        FreeSeats = c.Int(nullable: false),
                        HallId = c.Int(),
                        film_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Films", t => t.film_Id)
                .ForeignKey("dbo.Halls", t => t.HallId)
                .Index(t => t.HallId)
                .Index(t => t.film_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Sessions", "film_Id", "dbo.Films");
            DropForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.ActorFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.ActorFilm", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.GenreFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.GenreFilm", "GenreId", "dbo.Genres");
            DropIndex("dbo.ActorFilm", new[] { "FilmId" });
            DropIndex("dbo.ActorFilm", new[] { "ActorId" });
            DropIndex("dbo.GenreFilm", new[] { "FilmId" });
            DropIndex("dbo.GenreFilm", new[] { "GenreId" });
            DropIndex("dbo.Sessions", new[] { "film_Id" });
            DropIndex("dbo.Sessions", new[] { "HallId" });
            DropIndex("dbo.Halls", new[] { "CinemaId" });
            DropTable("dbo.ActorFilm");
            DropTable("dbo.GenreFilm");
            DropTable("dbo.Sessions");
            DropTable("dbo.Halls");
            DropTable("dbo.Cinemas");
            DropTable("dbo.Genres");
            DropTable("dbo.Films");
            DropTable("dbo.Actors");
        }
    }
}
