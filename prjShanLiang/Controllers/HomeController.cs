using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using System.Diagnostics;

namespace prjShanLiang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ShanLiang21Context db = new ShanLiang21Context(); 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       
            public IActionResult Index()
            {
                var Image = db.Stores.ToList();
                var Evaluates = db.StoreEvaluates.ToList();
                var model = new Tuple<List<Store>, List<StoreEvaluate>>(Image, Evaluates);
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