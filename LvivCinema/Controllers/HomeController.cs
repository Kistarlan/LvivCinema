using LvivCinema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvivCinema.Controllers
{
    public class HomeController : Controller
    {
        FilmContext db = new FilmContext();

        public ActionResult ListActorFilms(int? id)
        {

            if (id == null)
                return HttpNotFound();
            Actor actor = db.Actors.Find(id);
            if (actor == null)
                return HttpNotFound();
            return View(actor);
        }
        public ActionResult ActorList()
        {
            return View(db.Actors);
        }

        public ActionResult ListActorFilm()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateActor()
        {
            Actor actor = new Actor();
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActor([Bind(Include = "Id,Name,Surname,Year")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }
        [HttpGet]
        public ActionResult EditActor(int? id)
        {
            if (id == null)
                return HttpNotFound();
            Actor actor = db.Actors.Find(id);
            if (actor == null)
                return HttpNotFound();
            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditActor([Bind(Include = "Id,Name,Surname,Year")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }
        public ActionResult ListGenreFilms(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("GenreList");
            }
            Genre genre = db.Genres.Find(id);
            if (genre != null)
            {
                return View(genre);
            }
            return RedirectToAction("GenreList");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenreList()
        {
            return View(db.Genres);
        }
        public ActionResult FilmList()
        {
            return View(db.Films);
        }
        public ActionResult ShowInformation(int? Id)
        {

            if (Id == null)
            {
                return RedirectToAction("FilmList");
            }
            Film film = db.Films.Find(Id);
            if (film != null)
            {
                return View(film);
            }
            return RedirectToAction("FilmList");
        }

        [HttpGet]
        public ActionResult EditFilm(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("FilmList");
            }
            Film film = db.Films.Find(Id);
            if (film != null)
            {
                ViewBag.Actors = db.Actors.ToList();
                ViewBag.Genres = db.Genres.ToList();
                return View(film);
            }
            return RedirectToAction("FilmList");
        }
        [HttpPost]
        public ActionResult EditFilm(Film film, int[] selectedActors, int[] selectedGenres)
        {
            Film newFilm = db.Films.Find(film.Id);
            newFilm.Name = film.Name;
            newFilm.Description = film.Description;
            newFilm.Director = film.Director;
            newFilm.year = film.year;

            newFilm.Actors.Clear();
            if(selectedActors != null)
            {
                foreach (var actor in db.Actors.Where(thisfilm => selectedActors.Contains(thisfilm.Id)))
                    newFilm.Actors.Add(actor);
            }

            newFilm.Genres.Clear();
            if (selectedGenres != null)
            {
                foreach (var genre in db.Genres.Where(fi => selectedGenres.Contains(fi.Id)))
                    newFilm.Genres.Add(genre);
            }

            db.Entry(newFilm).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FilmList");
        }
        [HttpGet]
        public ActionResult CreateFilm()
        {
            ViewBag.Actors = db.Actors.ToList();
            ViewBag.Genres = db.Genres.ToList();
            Film film = new Film();
            return View(film);
        }

        [HttpPost]
        public ActionResult CreateFilm(Film film, int[] selectedGenres, int[] selectedActors)
        {

            db.Films.Add(film);
            if (selectedActors != null)
            {
                foreach (var actor in db.Actors.Where(thisfilm => selectedActors.Contains(thisfilm.Id)))
                    film.Actors.Add(actor);
            }

            if (selectedGenres != null)
            {
                foreach (var g in db.Genres.Where(fi => selectedGenres.Contains(fi.Id)))
                    film.Genres.Add(g);
            }
            
            db.SaveChanges();
            return RedirectToAction("FilmList");
        }

        public ActionResult CreateGenre()
        {
            Genre genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        public ActionResult CreateGenre(Genre genre)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
            return RedirectToAction("GenreList");
        }

        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DeleteFilm(int id)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            return View(film);
        }

        [HttpPost, ActionName("DeleteFilm")]
        public ActionResult DeleteConfirmedFilm(int id)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("FilmList");
        }
        [HttpGet]
        public ActionResult DeleteGenre(int? id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost, ActionName("DeleteGenre")]
        public ActionResult DeleteConfirmedGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("GenreList");
        }

        [HttpGet]
        public ActionResult DeleteActor(int? id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        [HttpPost, ActionName("DeleteActor")]
        public ActionResult DeleteConfirmedActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            db.Actors.Remove(actor);
            db.SaveChanges();
            return RedirectToAction("GenreList");
        }

        public ActionResult Contact()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}