using Microsoft.AspNetCore.Mvc;
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
        
        
        public IActionResult Index(int page = 1)
        {
            dynamic allData = new ExpandoObject();
            allData.News = GetAllNews();
            allData.Reviews = GetAllReviews();
            ViewBag.PageList = GetPagedNames(page);
            return View(allData);
        }
        protected IPagedList<dynamic> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;
            List<dynamic> AllData = new List<dynamic>();
            // retrieve list from database
            var news = _ctx.News.ToList();
            var reviews = _ctx.Reviews.ToList();
            foreach(var a in news)
            {
                AllData.Add(a);
            }
            foreach(var r in reviews)
            {
                AllData.Add(r);
            }
            AllData.OrderByDescending(el => el.Date);
            // page the list
            const int pageSize = 4;
            var listPaged = AllData.ToPagedList(page ?? 1, pageSize);
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;
            return listPaged;
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
