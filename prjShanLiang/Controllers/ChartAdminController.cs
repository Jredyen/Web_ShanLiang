using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace prjShanLiang.Controllers
{
    public class ChartAdminController : Controller
    {
        private readonly ShanLiang21Context _context;
        public ChartAdminController(ShanLiang21Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetStoreTypeCount()
        {
            var storeIds = _context.Stores.Select(s => s.StoreId).ToList();

            var resTypeCount = _context.StoreTypes
                .Where(st => storeIds.Contains((int)st.StoreId))
                .GroupBy(st => st.RestaurantTypeNumNavigation.TypeName)
                .Select(g => new { StoreType = g.Key, Count = g.Count() })
                .ToList();

            return Json(resTypeCount);
        }

        public IActionResult GetStorePocketCount() 
        {
            var memberIds = _context.Members.Select(m => m.MemberId).ToList();
            var storeIds = _context.Stores.Select(s => s.StoreId).ToList();

            var resPocketCount = _context.MemberActions
                .Where(a => memberIds.Contains((int)a.MemberId))
                .Where(a => storeIds.Contains((int)a.StoreId))
                .GroupBy(a => a.Store.RestaurantName)
                .Select(g => new { MemberAction = g.Key, Count = g.Count()})
                .ToList();

            return Json(resPocketCount);
        }
    }
}
