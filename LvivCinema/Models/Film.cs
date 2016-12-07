using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Film
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Director { set; get; }
        public int year { set; get; }
        public virtual ICollection<Genre> Genres { set; get; }
        public virtual ICollection<Actor> Actors { set; get; }
        public Film()
        {
            Genres = new List<Genre>();
            Actors = new List<Actor>();
        }

        
    }
}