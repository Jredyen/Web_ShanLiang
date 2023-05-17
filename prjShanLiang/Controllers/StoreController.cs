using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System;
using System.Linq;
using System.Text.Json;

namespace prjShanLiang.Controllers
{
    public class StoreController : Controller
    {
        private readonly ShanLiang21Context _db;
        public StoreController(ShanLiang21Context db)
        {
            _db = db;
        }

        public IActionResult List()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.StoreId select s;
            return View(datas);
        }
        public IActionResult Reconnend()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.Rating descending select s;
            return View(datas);
        }
        public IActionResult Latest()
        {
            IQueryable<Store> datas = from s in _db.Stores orderby s.StoreId descending select s;
            return View(datas);
        }
        public IActionResult Restaurant(int? id)
        {
            if (id == null)
                return RedirectToAction("Reconnend");
            CShowRestaurantViewModel datas = new CShowRestaurantViewModel();
            var sts = from s in _db.Stores.
                      Include(s => s.StoreEvaluates)
                      where s.StoreId == id
                      select s;
            var mbs = from m in _db.Members
                      orderby m.MemberId
                      select m;
            var sdp = from sd in _db.StoreDecorationImages where sd.StoreId == id select sd.ImagePath;
            var mfc = from ma in _db.MemberActions where ma.ActionId == 2 && ma.StoreId == id select ma;
            var smi = from sm in _db.StoreMealImages where sm.StoreId == id select sm.ImagePath;
            datas.store = sts;
            datas.member = mbs;
            datas.storeDecorationImagePath = sdp.FirstOrDefault();
            datas.memberFavorateCount = mfc.Count();
            datas.storeMealImages = smi;
            return View(datas);
        }
        public IActionResult GetRestaurantType(int id)
        {
            IQueryable datas = from s in _db.StoreTypes
                               join r in _db.RestaurantTypes
                               on s.RestaurantTypeNum equals r.RestaurantTypeNum
                               where s.StoreId == id
                               select new { s.No, s.RestaurantTypeNum, s.StoreId, r.TypeName };
            return Json(datas);
        }

        public IActionResult SearchRestaurantType(int? id)
        {
            IQueryable<Store> datas = from s in _db.StoreTypes.Include(s => s.Store)
                                      where s.RestaurantTypeNum == id
                                      orderby s.StoreId
                                      select s.Store;
            var data = from s in _db.RestaurantTypes
                       where s.RestaurantTypeNum == id
                       select s.TypeName;
            ViewBag.TypeName = data.FirstOrDefault();
            ViewBag.Id = id;
            return View(datas);
        }
        public IActionResult AddToFavorate()
        {
            string test = "";
            return Json(test);
        }

        public IActionResult GetName(string keyword)
        {
            IQueryable storeList = _db.Stores.Where(s => s.RestaurantName.Contains(keyword)).Select(s => s.RestaurantName);
            return Json(storeList);
        }
        public IActionResult ShowType()
        {
            IQueryable datas = _db.RestaurantTypes.Select(r => new { r.TypeName, r.RestaurantTypeNum });
            return Json(datas);
        }
        /// <summary>
        /// 使用篩選進行店家搜尋
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <param name="types">類型</param>
        /// <param name="districts">地區</param>
        /// <param name="rating">評級</param>
        /// <returns></returns>
        public IActionResult SearchStore(string keyword, string types, string districts, double? rating = 0, int order = 0)
        {
            //整合店名、類型與地區的搜尋
            //店名搜尋 : 完成
            //類型搜尋 : 完成
            //地區搜尋 : 完成
            //評價搜尋 : 完成

            IQueryable<Store> list = null;
            //如果關鍵字不是null的話就用關鍵字搜尋
            if (keyword != null)
            {
                list = _db.Stores.Where(s => s.RestaurantName.Contains(keyword));
            }
            else
            {
                list = _db.Stores.Select(s => s);
            }

            //有選取類型的話，把傳回來的類型字串變回陣列並防例外狀況，再篩選類型
            int[]? type = null;
            try
            {
                if (types != "undefined")
                    type = JsonSerializer.Deserialize<int[]>(types.Replace("\"", ""));
            }
            catch
            { }
            if (type != null && type.Length > 0)
            {
                list = list.Join(_db.StoreTypes, s => s.StoreId, st => st.StoreId, (s, st) => new { s, st })
                    .Where(x => type.Contains(x.st.RestaurantTypeNum))
                    .Select(x => x.s)
                    .Distinct();
            }

            //有選取地區的話，把傳回來的地區字串變回陣列並防例外狀況，再篩選地區
            int[]? district = null;
            try
            {
                if (districts != "undefined")
                    district = JsonSerializer.Deserialize<int[]>(districts.Replace("\"", ""));
            }
            catch
            { }
            if (district != null && district.Length > 0)
            {
                list = list.Where(s => district.Contains(s.DistrictId));
            }

            //如果評價不是0的話就篩選高於選取星數的
            if (rating != 0)
            {
                list = list.Where(s => s.Rating >= rating);
            }

            //只顯示需要給顧客瀏覽的資料
            var storeList = list.Select(s => new
            {
                s.RestaurantName,
                s.StoreId,
                s.Rating,
                s.OpeningTime,
                s.ClosingTime,
                s.RestaurantPhone,
                s.RestaurantAddress,
                imagePath = _db.StoreDecorationImages
                    .Where(x => x.StoreId == s.StoreId)
                    .Select(x => x.ImagePath).ToList(),
                typeName = _db.StoreTypes
                    .Where(st => st.StoreId == s.StoreId)
                    .Join(_db.RestaurantTypes,
            st => st.RestaurantTypeNum,
            rt => rt.RestaurantTypeNum,
            (st, rt) => rt.TypeName).ToList(),
            });

            //排序依據
            if (order == 1)
                storeList = from s in storeList orderby s.Rating descending select s;
            else if (order == 2)
                storeList = from s in storeList orderby s.Rating select s;
            else if (order == 3)
                storeList = from s in storeList orderby s.StoreId descending select s;
            else if (order == 4)
                storeList = from s in storeList orderby s.StoreId select s;

            return Json(storeList);
        }
    }
}
