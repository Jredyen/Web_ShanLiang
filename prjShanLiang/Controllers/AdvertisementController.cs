using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    public class AdvertisementController : Controller
    {

        private readonly ShanLiang21Context _adv;

        public AdvertisementController(ShanLiang21Context adv)
        {

            _adv = adv;
        }
        public IActionResult BuyAdv()
        { 
            
        
            var datas = from a in _adv.Stores
                        select a;
            return View(datas);
           
        }

      
    }
}
