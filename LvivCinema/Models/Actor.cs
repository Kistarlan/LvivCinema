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

        [Required(ErrorMessage ="Ім'я повинно бути обовязково")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Розмір імені повинен бути в межах [2-50] символів")]
        [Display(Name="Ім'я")]
        public string Name { set; get;}

        [Required(ErrorMessage = "Прізвище повинно бути обовязково")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Розмір прізвища повинен бути в межах [2-50] символів")]
        [Display(Name = "Прізвище")]
        public string Surname { set; get;}


        [Range(1900, 2016, ErrorMessage = "Неприпустимий рік")]
        [Display(Name = "Рік народження")]
        public int Year  { set; get;}

        [UIHint("Collection")]
        public virtual ICollection<Film> Films { set; get; }

        public Actor()
        {
            Films = new List<Film>();
        }
    }
}