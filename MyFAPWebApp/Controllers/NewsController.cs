using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFAPWebApp.Models;

namespace MyFAPWebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly MyFapContext myFapContext;
        public NewsController(MyFapContext myFapContext)
        {
            this.myFapContext = myFapContext;
        }
        // GET: NewsController
        public ActionResult Index()
        {
            var model= myFapContext.News.ToList();
            return View(model);
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var news= myFapContext.News.FirstOrDefault(m=>m.NewsId==id);
            if (news == null)
            {
                return NotFound(nameof(News));
            }
            return View(news);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                myFapContext.News.Add(news);
                myFapContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound() ;
            }
            var news = myFapContext.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                myFapContext.Update(news);
                myFapContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int? id)
        {
            var news = myFapContext.News.Find(id);
            myFapContext.News.Remove(news);
            myFapContext.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
