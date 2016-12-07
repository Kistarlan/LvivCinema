using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Cinema
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Adress { set; get; }

        public ICollection<Hall> Halls { get; set; }
        public Cinema()
        {
            Halls = new List<Hall>();
        }
    }
}