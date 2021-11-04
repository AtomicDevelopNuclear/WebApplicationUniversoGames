﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUniversoGames.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
