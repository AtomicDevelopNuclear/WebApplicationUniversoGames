using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationUniversoGames.Data;

namespace WebApplicationUniversoGames.Controllers
{
    public class MyController : Controller
    {
        private readonly DataContext _ctx;

        public MyController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public ActionResult MyAction(string search)
        {
            var azz = _ctx.News.Where(azz => azz.Title == search).ToList();
            //do whatever you need with the parameter, 
            //like using it as parameter in Linq to Entities or Linq to Sql, etc. 
            if (search is not null)
            {
                _ctx.News.Where(azz => azz.Title == search).ToList();
            }
            else { throw new Exception("HAI SBAGLIATO QUALCOSA"); }
            //Suppose your search result will be put in variable "result".
            ViewData.Model = azz;
            return View();
        }
    }
}
