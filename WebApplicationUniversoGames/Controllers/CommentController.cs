using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;
using WebApplicationUniversoGames.ViewModel;

namespace WebApplicationUniversoGames.Controllers
{
    public class CommentController : Controller
    {
        private readonly DataContext _ctx;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(DataContext ctx, UserManager<AppUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();//pagina corrispondente...
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, NewsCommentViewModel newComment)
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentNews = await _ctx.News.FindAsync(id);
                if (ModelState.IsValid)
                {
                    NewsComment comment = new NewsComment()
                    {
                        Content = newComment.Content,
                        Date = DateTime.Now,
                        UserId = _userManager.GetUserId(HttpContext.User),
                        NewsId = currentNews.Id,
                    };
                    _ctx.Add(comment);
                    await _ctx.SaveChangesAsync();
                    return RedirectToAction($"Details", "News", new { id = currentNews.Id});
                }
            }
            return View(newComment);
        }
    }
}
