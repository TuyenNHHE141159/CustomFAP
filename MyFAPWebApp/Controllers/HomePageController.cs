using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFAPWebApp.Models;

namespace MyFAPWebApp.Controllers
{
    public class HomePageController : Controller
    {
        private readonly MyFapContext context;

        public HomePageController(MyFapContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var top5News = context.News
    .OrderByDescending(n => n.CreatedDate)
    .Take(3)
    .Select(n => new {n.NewsId,n.CreatedDate, n.Title });
            List<News> list = new List<News>();
            foreach (var n in top5News)
            {
                News news = new News();
                news.NewsId = n.NewsId; 
                news.Title = n.Title;
                news.CreatedDate = n.CreatedDate;
                list.Add(news);
            }
            ViewData["top5News"] = list;
            return View();
        }
        public ActionResult LoadMoreNews(int skipCount)
        {
            var news = context.News.OrderByDescending(n => n.CreatedDate).Skip(skipCount).Take(3).ToList();
            if (news.Count == 0)
            {
                //return new EmptyResult();
            }
            return PartialView("_NewsList", news);
        }
        
    }
}
