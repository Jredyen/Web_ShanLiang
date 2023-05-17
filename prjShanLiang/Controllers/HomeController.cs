using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using System.Diagnostics;

namespace prjShanLiang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShanLiang21Context _db;
       
        public HomeController(ILogger<HomeController> logger, ShanLiang21Context db )
        {
            _logger = logger;
            _db = db;
        }

       
            public IActionResult Index()
            {

            //var datas = from s in _db.Stores.
            //            Include(s => s.StoreAdImages).
            //            Include(s => s.StoreEvaluates)

            //          select s;


            var Image = _db.Stores.ToList();
            var AdImages = _db.StoreAdImages.ToList();
            var Evaluate = _db.StoreEvaluates.ToList();
            var member = _db.Members.ToList();
            var blog = _db.Blogs.ToList();
            var model = new Tuple<List<Store>, List<StoreAdImage>,List<StoreEvaluate>,List<Member>,List<Blog>>(Image, AdImages, Evaluate, member,blog);
            return View(model);
            }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}