using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using System.Diagnostics;

namespace prjShanLiang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShanLiang21Context _adv;
       
        public HomeController(ILogger<HomeController> logger, ShanLiang21Context adv )
        {
            _logger = logger;
            _adv = adv;
        }

       
            public IActionResult Index()
            {
                var Image = _adv.Stores.ToList();
                var Evaluates = _adv.StoreEvaluates.ToList();
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