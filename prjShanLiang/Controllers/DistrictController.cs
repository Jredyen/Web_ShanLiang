using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Xml.Serialization;

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
                        select new { c.CityName, d.DistrictName, d.DistrictId };
            return datas;
        }
        //[HttpGet]
        //public object GG()
        //{
        //    var regions = _db.Regions
        //        .Include(r => r.Cities)
        //        .ThenInclude(c => c.Districts)
        //        .Select(r => new CRegion
        //        {
        //            regionName = r.RegionName,
        //            Cities = r.Cities.Select(c => new CCity
        //            {
        //                cityName = c.CityName,
        //                Districts = c.Districts.Select(d => new CDistrict
        //                {
        //                    districtName = d.DistrictName,
        //                    districtID = d.DistrictId
        //                }).ToList()
        //            }).ToList()
        //        })
        //        .ToList();
        //    return Json(regions);
        //}
    }
}
