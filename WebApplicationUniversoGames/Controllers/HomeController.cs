using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;
using X.PagedList;

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
        public IActionResult Index(string searchedString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchedString;
            
            IQueryable<ArticleCommons> allData = new List<ArticleCommons>().Concat(_ctx.News.ToList()).Concat(_ctx.Reviews.ToList()).AsQueryable();
            if (!String.IsNullOrEmpty(searchedString))
            {
                allData = allData.Where(s => s.Title.ToLower().Contains(searchedString.ToLower())  || s.Content.ToLower().Contains(searchedString.ToLower()));
            }
            int pageSize = 4;
            return View(PaginatedList<object>.Create(allData.OrderByDescending(x => x.Date).AsNoTracking(), pageNumber ?? 1, pageSize));
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
