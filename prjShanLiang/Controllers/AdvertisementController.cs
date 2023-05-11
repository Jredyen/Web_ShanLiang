using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Text.Json;

namespace prjShanLiang.Controllers
{
    public class AdvertisementController : Controller
    {

        private readonly ShanLiang21Context _db;

        public AdvertisementController(ShanLiang21Context db)
        {

            _db = db;
        }
        public IActionResult BuyAdv()
        {


            var stores = _db.Stores.Take(2).ToList();
            var AdImages = _db.StoreAdImages.ToList();
            var model = new Tuple<List<Store>, List<StoreAdImage>>(stores, AdImages);
            return View(model);
           
        }
      


    }
}
