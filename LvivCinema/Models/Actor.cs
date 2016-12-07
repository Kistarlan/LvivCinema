using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Actor
    {
        public int Id  { set; get;}
        public string Name { set; get;}
        public string Surname { set; get;}
        public int Year  { set; get;}
        public virtual ICollection<Film> Films { set; get; }

        public Actor()
        {
            Films = new List<Film>();
        }
    }
}