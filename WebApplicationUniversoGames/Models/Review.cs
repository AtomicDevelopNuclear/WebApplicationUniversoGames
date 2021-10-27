using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Rev { get; set; }
        public double Score { get; set; }
        public string Image { get; set; }
        public Admin Admin { get; set; }
    }
}
