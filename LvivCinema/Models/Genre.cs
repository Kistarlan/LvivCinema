using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Genre
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public virtual ICollection<Film> Films { set; get; }

        public Genre()
        {
            Films = new List<Film>();
        }
    }
}