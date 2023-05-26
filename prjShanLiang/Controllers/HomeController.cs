using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using System.Diagnostics;

namespace prjShanLiang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShanLiang21Context _db;
       
        public HomeController(ShanLiang21Context db )
        {
            _db = db;
        }

       
            public IActionResult Index()
            {

            var Image = _db.Stores.ToList();
            var AdImages = _db.StoreAdImages.Where(sa => sa.ADColumn == "true").ToList();
            var Evaluate = _db.StoreEvaluates.ToList();
            var member = _db.Members.ToList();
            var blog = _db.Blogs.ToList();
            var MemAction = _db.MemberActions.Where(Ma=>Ma.ActionId==2).ToList();
            var model = new Tuple<List<Store>, List<StoreAdImage>,List<StoreEvaluate>,List<Member>,List<Blog>,List<MemberAction>>(Image, AdImages, Evaluate, member,blog, MemAction);
            return View(model);


            }

     
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}