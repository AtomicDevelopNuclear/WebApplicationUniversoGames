using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;
using X.PagedList;

namespace WebApplicationUniversoGames.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly DataContext _ctx;

        public ReviewsController(DataContext ctx)
        {
            _ctx = ctx;
        }

        // vecchia funzione per richiamare le news
        //public IActionResult Index()
        //{
        //    return View(_ctx.Reviews);
        //}

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageList = GetPagedNames(page);
            return View();
        }
        protected IPagedList<Review> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database
            var reviews = _ctx.Reviews.ToList();
            // page the list
            const int pageSize = 4;
            var listPaged = reviews.ToPagedList(page ?? 1, pageSize);
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;
            return listPaged;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                _ctx.Reviews.Add(review);
                _ctx.SaveChanges();
                return Redirect("Index");
            }
            return View(review);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var obj = _ctx.Reviews.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeleteReviews(int? id)
        {
            var obj = _ctx.Reviews.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            _ctx.Reviews.Remove(obj);
            _ctx.SaveChanges();
            return Redirect("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var obj = _ctx.Reviews.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Review review)
        {
            if (ModelState.IsValid) // se il form e' valido
            {
                _ctx.Reviews.Update(review);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        //httpget to view Details
        public IActionResult Details(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var newsDetails = _ctx.Reviews.Find(id);
            if (newsDetails is null)
            {
                return NotFound();
            }
            return View(newsDetails);
        }

    }
}
