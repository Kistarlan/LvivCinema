using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvivCinema.Models
{
    public class Session
    {
        public int Id { set; get; }
        public DateTime dataTime { set; get; }
        public int FreeSeats { set; get; }
        public Film film { set; get; }

        public int? HallId { set; get; }
        public Hall hall { set; get; }
    }
}