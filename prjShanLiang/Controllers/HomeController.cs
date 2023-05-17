using Microsoft.AspNetCore.Mvc;
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
                var Image = _db.Stores.Take(10).ToList();
                var AdImages = _db.StoreAdImages.ToList();
                var model = new Tuple<List<Store>, List<StoreAdImage>>(Image, AdImages);
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