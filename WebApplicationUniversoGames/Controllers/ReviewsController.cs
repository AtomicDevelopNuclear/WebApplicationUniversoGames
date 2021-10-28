using Microsoft.AspNetCore.Mvc;
using System;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;

namespace WebApplicationUniversoGames.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly DataContext _ctx;

        public ReviewsController(DataContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View(_ctx.Reviews);
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
    }
}
