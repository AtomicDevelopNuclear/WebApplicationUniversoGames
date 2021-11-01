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
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength =2)]
        public string Title { get; set; }
        [Required]
        public Categories Category { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 2)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public string CoverImage { get; set; }
    }
}
