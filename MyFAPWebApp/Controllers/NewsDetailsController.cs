using Microsoft.AspNetCore.Mvc;
using MyFAPWebApp.Models;

namespace MyFAPWebApp.Controllers
{
    
    public class NewsDetailsController : Controller
    {
        private readonly MyFapContext context = new();
        public IActionResult Index(int news_id)
        {
            if(news_id != 0)
            {
                News news = context.News.Find(news_id);
                if(news != null)
                {
                    
                    var top5News = context.News
    .OrderByDescending(n => n.CreatedDate)
    .Take(5)
    .Select(n => new { n.NewsId, n.CreatedDate, n.Title });
                    List<News> list = new List<News>();
                    foreach (var n in top5News)
                    {
                        News ne = new News();
                        ne.NewsId = n.NewsId;
                        ne.Title = n.Title;
                        ne.CreatedDate = n.CreatedDate;
                        list.Add(ne);
                    }
                    ViewData["News"] = news;
                    ViewData["top5News"] = list;
                }
            }  
            return View();
        }
    }
}
