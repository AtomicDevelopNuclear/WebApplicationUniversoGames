using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 50)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
