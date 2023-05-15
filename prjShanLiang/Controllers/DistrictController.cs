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
        //public IQueryable GetR()
        //{
        //    ShanLiang21Context _db = new();
        //    var R = _db.Regions.Select(x => x).ToList();
        //    var C = _db.Cities.Select(x => x).ToList();
        //    var D = _db.Districts.Select(x => x).ToList();

        //    foreach (var r in R)
        //    {
        //    var datas = new List<CRegion>
        //    {
        //        new CRegion
        //        {
        //            regionName = r.RegionName,
        //            Cities = new List<CCity>
        //            {
        //                new CCity
        //                {
        //                    cityName = "A市",
        //                    Districts = new List<CDistrict>
        //                    {
        //                        new CDistrict { districtName = "1區" },
        //                        new CDistrict { districtName = "2區" },
        //                    }
        //                },
        //                new CCity
        //                {
        //                    cityName = "B縣",
        //                    Districts = new List<CDistrict>
        //                    {
        //                        new CDistrict { districtName = "3區" },
        //                        new CDistrict { districtName = "4區" },
        //                    }
        //                },
        //            }
        //        },
        //        new CRegion
        //        {
        //            regionName = "南部",
        //            Cities = new List<CCity>
        //            {
        //                new CCity
        //                {
        //                    cityName = "C市",
        //                    Districts = new List<CDistrict>
        //                    {
        //                        new CDistrict { districtName = "5區" },
        //                        new CDistrict { districtName = "6區" },
        //                    }
        //                },
        //                new CCity
        //                {
        //                    cityName = "D市",
        //                    Districts = new List<CDistrict>
        //                    {
        //                        new CDistrict { districtName = "7區" },
        //                        new CDistrict { districtName = "8區" },
        //                    }
        //                },
        //            }
        //        },
        //    };
        //    }
        //    return Json(datas);
        //}
    }
}
