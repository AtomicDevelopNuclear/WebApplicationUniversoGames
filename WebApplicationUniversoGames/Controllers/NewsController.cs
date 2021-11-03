using Microsoft.AspNetCore.Hosting;
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
    //Comment
    public class NewsController : Controller
    {

        //Dependence Injection Direttamente nei controller perchè non abbiamo i services
        private readonly DataContext _ctx;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(DataContext ctx, IWebHostEnvironment hostEnvironment)
        {
            _ctx = ctx;
            _hostEnvironment = hostEnvironment;
        }
        /*
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageList = GetPagedNames(page);
            return View();
        }
        protected IPagedList<News> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database
            var news = _ctx.News.ToList();
            // page the list
            const int pageSize = 4;
            var listPaged = news.ToPagedList(page ?? 1, pageSize);
            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;
            return listPaged;
        }
        */
        public async Task<IActionResult>Index(string searchedString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchedString;
            if(searchedString != null)
            {
                pageNumber = 1;
            }
            var news =_ctx.News.AsQueryable();
            if (!String.IsNullOrEmpty(searchedString))
            {
                news = news.Where(s => s.Title.ToLower().Contains(searchedString.ToLower()) || s.Content.ToLower().Contains(searchedString.ToLower()));
            }
            int pageSize = 4;
            return View(await PaginatedList<News>.CreateAsync(news.OrderByDescending(x=>x.Date).AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //httpget to view Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var newsDetails = await _ctx.News.FirstOrDefaultAsync(n => n.Id == id);
            var newsViewModel = new NewsViewModel()
            {
                Id = newsDetails.Id,
                Title = newsDetails.Title,
                Category = newsDetails.Category,
                ExistingImage = newsDetails.Image,
                Content = newsDetails.Content,
                Date = newsDetails.Date
            };
            if (newsDetails is null)
            {
                return NotFound();
            }
            return View(newsDetails);
        }
        //GetCreateNews
        public IActionResult Create()
        {
            return View();//pagina corrispondente...
        }

        //PostFunction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(NewsViewModel newArticle)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(newArticle);
                News newNews = new News()
                {
                    Id = newArticle.Id,
                    Title = newArticle.Title,
                    Category = newArticle.Category,
                    Image = uniqueFileName,
                    Content = newArticle.Content,
                    Date = DateTime.UtcNow
                };
                _ctx.Add(newNews);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newArticle);
        }

        public async Task<IActionResult>Update(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }
            var article = await _ctx.News.FindAsync(id);
            var newsViewModel = new NewsViewModel()
            {
                Id = article.Id,
                Title = article.Title,
                Category = article.Category,
                ExistingImage = article.Image,
                Content = article.Content,
                Date = article.Date
            };
            if(article is null)
            {
                return NotFound();
            }
            return View(newsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Update(int id, NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newArticle = await _ctx.News.FindAsync(model.Id);
                newArticle.Title = model.Title;
                newArticle.Category = model.Category;
                newArticle.Content = model.Content;
                if(model.UploadedImage != null)
                {
                    if(model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "NewsUploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    newArticle.Image = ProcessUploadedFile(model);
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
            if(id is null)
            {
                return NotFound();
            }
            var news = await _ctx.News.FirstOrDefaultAsync(n => n.Id == id);
            var newsViewModel = new NewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Category = news.Category,
                Content = news.Content,
                ExistingImage = news.Image
            };
            if(news is null)
            {
                return NotFound();
            }
            return View(newsViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var news = await _ctx.News.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", news.Image);
            _ctx.News.Remove(news);
            if(await _ctx.SaveChangesAsync() > 0)
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

        private string ProcessUploadedFile(NewsViewModel model)
        {
            string uniqueFileName = null;

            if (model.UploadedImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "NewsUploads");
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
