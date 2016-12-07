using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Hall
    {
        public int Id { set; get; }
        public int Number { set; get; }

        public int NumberSeats {set; get;}
        
        public int? CinemaId { set; get; }
        public Cinema cinema { set; get; }

        public ICollection<Session> Sessions { get; set; }
        public Hall()
        {
            Sessions = new List<Session>();
        }
    }
}