using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Models;

namespace WebApplicationUniversoGames.ViewModel
{
    public class NewsCommentViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ReviewId { get; set; }
        public Review LinkedReview { get; set; }
    }
}
