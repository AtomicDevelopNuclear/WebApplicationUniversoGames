using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Enums;

namespace WebApplicationUniversoGames.Models
{
    public class Review : ArticleCommons
    {
        
        public Categories Category { get; set; }
        
        [Required]
        public double Score { get; set; }
        [Required]
        public string CoverImage { get; set; }
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }
    }
}
