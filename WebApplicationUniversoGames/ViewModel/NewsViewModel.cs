using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Enums;

namespace WebApplicationUniversoGames.ViewModel
{
    public class NewsViewModel : EditImageViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public Categories Category { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 50)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
