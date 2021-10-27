using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublish { get; set; }
        public Admin Admin { get; set; }
    }
}
