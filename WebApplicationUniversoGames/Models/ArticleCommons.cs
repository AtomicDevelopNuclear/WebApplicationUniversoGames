using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class ArticleCommons
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
