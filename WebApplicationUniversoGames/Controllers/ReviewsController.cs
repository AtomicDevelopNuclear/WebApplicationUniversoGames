﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;
using WebApplicationUniversoGames.ViewModel;
using X.PagedList;

namespace WebApplicationUniversoGames.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly DataContext _ctx;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReviewsController(DataContext ctx, IWebHostEnvironment hostEnvironment)
        {
            _ctx = ctx;
            _hostEnvironment = hostEnvironment;
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var reviewDetails = await _ctx.Reviews.FirstOrDefaultAsync(n => n.Id == id);
            var reviewsViewModel = new ReviewsViewModel()
            {
                Id = reviewDetails.Id,
                Title = reviewDetails.Title,
                Category = reviewDetails.Category,
                ExistingImage = reviewDetails.CoverImage,
                Score = reviewDetails.Score,
                Content = reviewDetails.Content,
                Date = reviewDetails.Date
            };
            if (reviewDetails is null)
            {
                return NotFound();
            }
            return View(reviewDetails);
        }
        //GetCreateNews
        public IActionResult Create()
        {
            return View();//pagina corrispondente...
        }

        //PostFunction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Review newReview = new Review()
                { 
                    Id = model.Id,
                    Title = model.Title,
                    Category = model.Category,
                    CoverImage = uniqueFileName,
                    Score = model.Score,
                    Content = model.Content,
                    Date = DateTime.UtcNow
                };
                _ctx.Add(newReview);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var article = await _ctx.Reviews.FindAsync(id);
            var reviewViewModel = new ReviewsViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Category = article.Category,
                ExistingImage = article.CoverImage,
                Score = article.Score,
                Content = article.Content,
                Date = article.Date
            };
            if (article is null)
            {
                return NotFound();
            }
            return View(reviewViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ReviewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newArticle = await _ctx.Reviews.FindAsync(model.Id);
                newArticle.Title = model.Title;
                newArticle.Category = model.Category;
                newArticle.Content = model.Content;
                newArticle.Score = model.Score;
                if (model.UploadedImage != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "ReviewsUploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    newArticle.CoverImage = ProcessUploadedFile(model);
                }
                _ctx.Update(newArticle);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //GetDelete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var review = await _ctx.Reviews.FirstOrDefaultAsync(n => n.Id == id);
            var reviewViewModel = new ReviewsViewModel()
            {
                Id = review.Id,
                Title = review.Title,
                Category = review.Category,
                Content = review.Content,
                Score = review.Score,
                ExistingImage = review.CoverImage
            };
            if (review is null)
            {
                return NotFound();
            }
            return View(reviewViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _ctx.Reviews.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", review.CoverImage);
            _ctx.Reviews.Remove(review);
            if (await _ctx.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _ctx.News.Any(n => n.Id == id);
        }

        private string ProcessUploadedFile(ReviewsViewModel model)
        {
            string uniqueFileName = null;

            if (model.UploadedImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "ReviewsUploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadedImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
