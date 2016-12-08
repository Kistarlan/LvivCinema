using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Cinema
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Ім'я повинно бути обовязково")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Розмір імені повинен бути в межах [2-50] символів")]
        [Display(Name = "Назва")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Адреса повинна бути вказана")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Розмір назви адреси повинен бути в межах [10-1000] символів")]
        [Display(Name = "Адреса")]
        public string Adress { set; get; }

        public ICollection<Hall> Halls { get; set; }
        public Cinema()
        {
            Halls = new List<Hall>();
        }
    }
}