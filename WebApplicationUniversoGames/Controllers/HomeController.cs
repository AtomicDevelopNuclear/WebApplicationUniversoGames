using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        /*
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
        
        // page the list
        const int pageSize = 4;
            var listPaged = AllData.ToPagedList(page ?? 1, pageSize);
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;
            return listPaged;
        }
        */
        public IActionResult Index(string searchedString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchedString;

            IQueryable<ArticleCommons> allData = new List<ArticleCommons>().Concat(_ctx.News.ToList()).Concat(_ctx.Reviews.ToList()).AsQueryable();

            if (!String.IsNullOrEmpty(searchedString))
            {
                allData = allData.Where(s => s.Title.ToLower().Contains(searchedString.ToLower()) || s.Content.ToLower().Contains(searchedString.ToLower()));
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
