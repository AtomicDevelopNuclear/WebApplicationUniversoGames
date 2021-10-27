using Microsoft.AspNetCore.Mvc;
using System;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;

namespace WebApplicationUniversoGames.Controllers
{
    public class NewsController : Controller
    {

        //Dependence Injection Direttamente nei controller perchè non abbiamo i services
        private readonly DataContext _ctx;

        public NewsController(DataContext ctx)
        {
            _ctx = ctx;
        }

        //Ogni IAction è un'azione che permette di...
        public IActionResult Index()
        {
            return View(_ctx.News);
        }

        //GetCreateNews
        public IActionResult Create()
        {
            return View();//pagina corrispondente...
        }

        //PostFunction
        [HttpPost]
        public IActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                news.DateOfPublish = DateTime.Now;
                _ctx.News.Add(news);
                _ctx.SaveChanges();
                return Redirect("Index");
            }
            return View(news);
        }

        //GetDelete
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var obj = _ctx.News.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeleteNews(int? id)
        {
            var obj = _ctx.News.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            _ctx.News.Remove(obj);
            _ctx.SaveChanges();
            return Redirect("Index");
        }

        //GetUpdate
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var obj = _ctx.News.Find(id);
            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(News news)
        {
            if (ModelState.IsValid)
            {
                news.DateOfPublish = DateTime.Now;
                _ctx.News.Update(news);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
    }
}
