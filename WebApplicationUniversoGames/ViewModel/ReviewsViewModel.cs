using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Enums;
using WebApplicationUniversoGames.Models;

namespace WebApplicationUniversoGames.ViewModel
{
    public class ReviewsViewModel : EditImageViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public Categories Category { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 2)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public double Score { get; set; }
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }
    }
}
