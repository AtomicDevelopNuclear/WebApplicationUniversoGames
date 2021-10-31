﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;
using WebApplicationUniversoGames.Models;
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

        //GetCreateNews
        public IActionResult Create()
        {
            return View();//pagina corrispondente...
        }

        //PostFunction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create( News newArticle)
        {
            if (ModelState.IsValid)
            {
                string folder = "News/Image";
                folder += Guid.NewGuid().ToString() + "_" + newArticle.ImageFile.FileName;
                newArticle.Image = folder;
                string serverfolder = Path.Combine(_hostEnvironment.WebRootPath, folder);
                await newArticle.ImageFile.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
                newArticle.DateOfPublish = DateTime.Now;
                _ctx.Add(newArticle);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newArticle);
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
                _ctx.News.Update(news);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
        //httpget to view Details
        public IActionResult Details(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }
            var newsDetails = _ctx.News.Find(id);
            if(newsDetails is null)
            {
                return NotFound();
            }
            return View(newsDetails);
        }
    }
}
