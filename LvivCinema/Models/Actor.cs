using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvivCinema.Models
{
    public class Actor
    {
        [ScaffoldColumn(false)]
        public int Id  { set; get;}

        [Display(Name="Ім'я")]
        public string Name { set; get;}
        [Display(Name = "Прізвище")]
        public string Surname { set; get;}
        [Display(Name = "Рік народження")]
        public int Year  { set; get;}

        [Display(Name = "Фільми")]
        public virtual ICollection<Film> Films { set; get; }

        public Actor()
        {
            Films = new List<Film>();
        }
    }
}