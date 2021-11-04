using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class ReviewComment : Comment
    {
        public int ReviewId { get; set; }
        public Review LinkedReview { get; set; }
    }
}
