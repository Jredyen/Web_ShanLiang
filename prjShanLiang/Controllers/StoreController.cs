using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;
using System;
using System.Linq;

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
            ViewBag.Id = id;
            if (id == null)
                return RedirectToAction("Reconnend");
            IQueryable datas = from s in _db.Stores.Include(s => s.StoreDecorationImages).Include(s => s.StoreEvaluates).Include(s => s.MemberActions)
                               where s.StoreId == id
                               select s;
            if (datas == null)
                return RedirectToAction("Reconnend");
            return View(datas);  
        }
        public IActionResult GetStore(string keyword)
        {
            IQueryable storeList = _db.Stores.Where(s => s.RestaurantName.Contains(keyword)).Select(s => s.RestaurantName);
            return Json(storeList);
        }
        public IActionResult ShowType()
        {
            IQueryable datas = _db.RestaurantTypes.Select(r => new { r.TypeName , r.RestaurantTypeNum});
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
        public IActionResult SearchStore(string keyword, int[]? types, int[]? districts = null, double? rating = 0)
        {
            //測試用            
            //types = new int[] { 4, 5, 6 };
            //districts = new int[] { 5, 6, 7 };

            //TODO:整合店名、類型與地區的搜尋
            //店名搜尋 : 完成
            //類型搜尋 : 未完成且無法帶入
            //地區搜尋 : 未完成且無法帶入
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

            //如果有選取類型的話就再篩選類型
            //if (types != null && types.Length > 0)
            //{
            //    list = list.Join(_db.StoreTypes, s => s.StoreId, st => st.StoreId, (s, st) => new { s, st })
            //        .Where(x => types.Contains(x.st.RestaurantTypeNum))
            //        .Select(x => x.s)
            //        .Distinct();
            //}

            //如果有選取地區的話就再篩選地區
            //if (districts != null && districts.Length > 0)
            //{
            //    list = list.Where(s => districts.Contains(s.DistrictId));
            //}

            //如果評價不是0的話就篩選高於的
            /*TODO:改成評價範圍
            1 = 1.9以下
            2 = 2.5以下
            3 = 3.5以下
            4 = 4.5以下
            5 = 4.6以上
            */
            if (rating != 0)
            {
                list = list.Where(s => s.Rating >= rating);
            }

            //只顯示指定的資料
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
            return Json(storeList);
        }
    }
}
