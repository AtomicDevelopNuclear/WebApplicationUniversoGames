using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;

namespace WebApplicationUniversoGames.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _ctx;
        public HomeController(DataContext ctx)
        {
            _ctx = ctx;
        }
        private List<News> GetAllNews()
        {
            return _ctx.News.ToList();
        }
        private List<Review> GetAllReviews()
        {
            return _ctx.Reviews.ToList();
        }
        public IActionResult Index(int? page)
        {
            var customModel = new Tuple<List<News>, List<Review>>(GetAllNews(), GetAllReviews());
            return View(customModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
