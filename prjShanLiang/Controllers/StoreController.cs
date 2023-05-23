using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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
            IEnumerable<Member> mbs = from m in _db.Members
                                      orderby m.MemberId
                                      select new Member { MemberId = m.MemberId, MemberName = m.MemberName };
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
        public IActionResult ShowFavorate(int id)
        {
            MemberAction ma = null;
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
                return Json(ma);
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            ma = _db.MemberActions.Where(ma => ma.ActionId == 2 && ma.MemberId == mem.MemberId && ma.StoreId == id).FirstOrDefault();
            return Json(ma);
        }
        public IActionResult AddToFavorate(int id)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            var ma = _db.MemberActions.Where(ma => ma.ActionId == 2 && ma.MemberId == mem.MemberId && ma.StoreId == id).FirstOrDefault();
            if (ma != null)
            {
                _db.MemberActions.Remove(ma);
                _db.SaveChanges();
                ma.MemberNotes = "";
                return Json(ma);
            }
            else if (ma == null)
            {
                ma = new MemberAction();
                ma.ActionId = 2;
                ma.MemberId = mem.MemberId;
                ma.StoreId = id;
                ma.MemberNotes = "新增收藏";
                _db.MemberActions.Add(ma);
                _db.SaveChanges();
                return Json(ma);
            }
            else
                return RedirectToAction("Login", "User");
        }
        public IActionResult AddComment(int? id)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            ViewBag.Id = id;
            ViewBag.mid = mem.MemberId;
            return View();
        }
        [HttpPost]
        public IActionResult AddComment(StoreEvaluate se)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            var se1 = _db.StoreEvaluates.Where(se => se.MemberId == mem.MemberId).FirstOrDefault();
            if (se1 == null)
            {
                _db.StoreEvaluates.Add(se);
                _db.SaveChanges();
                return RedirectToAction("Restaurant", "Store", new { id = se.StoreId });
            }
            else
            {
                se1.Comments = se.Comments;
                se1.Rating = se.Rating;
                se1.EvaluateDate = se.EvaluateDate;
                _db.SaveChanges();
                return RedirectToAction("Restaurant", "Store", new { id = se.StoreId });
            }
        }
        public IActionResult Reserve(int? id)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            ViewBag.Id = id;
            ViewBag.mid = mem.MemberId;
            return View();
        }
        [HttpPost]
        public IActionResult Reserve(StoreReserved sr)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            var s = _db.Stores.Where(s => s.StoreId == sr.StoreId).FirstOrDefault();
            var sr1 = _db.StoreReserveds.Where(s => s.StoreId == sr.StoreId).Select(s => s);
            var gsr1 = sr1.GroupBy(sr => sr.Time, sr => sr.NumOfPeople, (time, num) => new
            {
                Time = time,
                Sum = num.Sum()
            });
            if (gsr1.Where(g => g.Time == sr.Time).FirstOrDefault()?.Sum >= s.Seats)
            {
                // 跳出視窗：該時段已滿
                sr.NumOfPeople = 0;
                return Json(sr);
            }
            else
            {
                _db.StoreReserveds.Add(sr);
                _db.SaveChanges();
                return RedirectToAction("Restaurant", "Store", new { id = sr.StoreId });
            }

        }
        public IActionResult GetName(string keyword)
        {
            IQueryable storeList = _db.Stores.Where(s => s.RestaurantName.Contains(keyword) && s.AccountStatus == 1).Select(s => s.RestaurantName);
            return Json(storeList);
        }
        public IActionResult GetType()
        {
            IQueryable datas = _db.RestaurantTypes.Select(rt => new 
            { 
                rt.TypeName, 
                rt.RestaurantTypeNum, 
                Qty = _db.StoreTypes
                .Join(_db.Stores, st => st.StoreId, s => s.StoreId, (st, s) => new { st, s })
                .Where(x => x.st.RestaurantTypeNum == rt.RestaurantTypeNum && x.s.AccountStatus == 1)
                .Count()
            });
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
        public IActionResult SearchStore(string keyword, string types, string districts, double? rating = 0, int order = 0, int step = 0, int take = 1000)
        {
            try
            {
                //整合店名、類型與地區的搜尋
                //店名搜尋 : 完成
                //類型搜尋 : 完成
                //地區搜尋 : 完成
                //評價搜尋 : 完成

                IQueryable<Store> list = null;
                //如果關鍵字不是null的話就用關鍵字搜尋，且要是審核通過的店家
                if (keyword != null)
                {
                    list = _db.Stores.Where(s => s.RestaurantName.Contains(keyword) && s.AccountStatus == 1);
                }
                else
                {
                    list = _db.Stores.Where(s => s.AccountStatus == 1).Select(s => s);
                }

                //有選取類型的話，把傳回來的類型字串變回陣列並防例外狀況，再篩選類型
                int[]? type = null;
                try
                {
                    if (types != "undefined" && types != null)
                        type = JsonSerializer.Deserialize<int[]>(types.Replace("\"", ""));
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
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
                    if (districts != "undefined" && districts != null)
                        district = JsonSerializer.Deserialize<int[]>(districts.Replace("\"", ""));
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
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
                int datasum = list.Count();
                var storeList = list.Select(s => new
                {
                    s.RestaurantName,
                    s.StoreId,
                    s.Rating,
                    s.OpeningTime,
                    s.ClosingTime,
                    s.RestaurantPhone,
                    s.RestaurantAddress,
                    s.Latitude,
                    s.Longitude,
                    imagePath = _db.StoreDecorationImages
                        .Where(x => x.StoreId == s.StoreId)
                        .Select(x => x.ImagePath).ToList(),
                    typeName = _db.StoreTypes
                        .Where(st => st.StoreId == s.StoreId)
                        .Join(_db.RestaurantTypes,
                st => st.RestaurantTypeNum,
                rt => rt.RestaurantTypeNum,
                (st, rt) => rt.TypeName).ToList(),
                    datasum,
                    like = _db.MemberActions
                    .Where(ma => ma.StoreId == s.StoreId && ma.ActionId == 2)
                    .Count()
                }).Skip(step * 10).Take(take);

                //if (step > 0)
                //    storeList.Skip(step * 10).Take(10);
                //else
                //    storeList.Take(10);

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
            catch (Exception ex)
            {
                return Json("系統錯誤 : " + ex.Message);
            }
        }

        public object GetRACAD()
        {
            var regions = _db.Regions
                .Include(r => r.Cities)
                .ThenInclude(c => c.Districts)
                .Select(r => new CRegion
                {
                    regionName = r.RegionName,
                    Cities = r.Cities.Select(c => new CCity
                    {
                        cityName = c.CityName,
                        Districts = c.Districts.Select(d => new CDistrict
                        {
                            districtName = d.DistrictName,
                            districtID = d.DistrictId
                        }).ToList()
                    }).ToList()
                })
                .ToList();
            return Json(regions);
        }

        public IActionResult getpoint()
        {
            IQueryable datas = _db.Stores.Select(s => new
            {
                s.Latitude,
                s.Longitude
            });
            return Json(datas);
        }
    }
}
