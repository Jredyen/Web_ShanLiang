using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;

namespace prjShanLiang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        [HttpGet]
        public IQueryable GetDistrict()
        {
            ShanLiang21Context _db = new();
            var datas = from c in _db.Cities
                        join d in _db.Districts
                        on c.CityId equals d.CityId
                        select new { c.CityName, d.DistrictName, d.DistrictId};
            return datas;
        }
        //[HttpGet]
        //public IQueryable GetShowType()
        //{
        //    ShanLiang21Context _db = new();
        //    var datas = from c in _db.RestaurantTypes select new { c.TypeName };
        //    return datas;
        //}
    }
}
