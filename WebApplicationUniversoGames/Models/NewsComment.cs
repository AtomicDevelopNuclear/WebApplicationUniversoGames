using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class NewsComment : Comment
    {
        public int NewsId { get; set; }
        public News LinkedArticle { get; set; }
    }
}
