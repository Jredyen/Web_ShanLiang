using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return View();
        }
        public IActionResult Restaurant(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CShowRestaurantViewModel datas = new();
            var sts = from s in _db.Stores
                      where s.StoreId == id
                      select s;
            //IEnumerable<Member> mbs = from m in _db.Members
            //                          orderby m.MemberId
            //                          select new Member { MemberId = m.MemberId, MemberName = m.MemberName };
            var se = from e in _db.StoreEvaluates?.Include(m => m.Member)
                     where e.StoreId == id
                     select e;
            var sdp = from sd in _db.StoreDecorationImages where sd.StoreId == id select sd.ImagePath;//餐廳封面照
            var mfc = from ma in _db.MemberActions where ma.ActionId == 2 && ma.StoreId == id select ma;//收藏總數
            var smi = from mm in _db.MealMenus where mm.StoreId == id select mm.MealImagePath;//改抓MealMenu.MealImagePath
            datas.store = sts;
            //datas.member = mbs;
            datas.storeEvaluates = se;
            datas.storeDecorationImagePath = sdp.FirstOrDefault();
            datas.memberFavorateCount = mfc.Count();
            datas.storeMealImages = smi;//改抓MealMenu.MealImagePath
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
        }//顯示餐廳類別超連結
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
        }//餐廳類別頁面
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
                return Json(new { memberNotes = "未登入" });
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);
            var ma = _db.MemberActions.Where(ma => ma.ActionId == 2 && ma.MemberId == mem.MemberId && ma.StoreId == id).FirstOrDefault();
            IQueryable<MemberAction> mfc = null; //計數用
            if (ma != null)
            {
                _db.MemberActions.Remove(ma);
                _db.SaveChanges();
                ma.MemberNotes = "";
                mfc = from m in _db.MemberActions where m.ActionId == 2 && m.StoreId == id select m;
                return Json(new { memberNotes = ma.MemberNotes, count = mfc.Count() });
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
                mfc = from m in _db.MemberActions where m.ActionId == 2 && m.StoreId == id select m;
                return Json(new { memberNotes = ma.MemberNotes, count = mfc.Count() });
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
        public async Task<IActionResult> AddComment([FromBody] StoreEvaluate se)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                Json(new { type = 0 });
                //return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);

            var se1 = _db.StoreEvaluates.Where(se1 => se1.MemberId == mem.MemberId && se1.StoreId == se.StoreId).FirstOrDefault();// 抓[登入會員]在[這間店]是否有過評論
            if (se1 == null)
            {
                _db.StoreEvaluates.Add(se);
                _db.SaveChanges();
                return Json(new { type = 1 });
                //return RedirectToAction("Restaurant", "Store", new { id = se.StoreId });
            }// 沒有評論的場合:新增
            else
            {
                se1.Comments = se.Comments;
                se1.Rating = se.Rating;
                se1.EvaluateDate = se.EvaluateDate;
                _db.SaveChanges();

                var storeRating = _db.StoreEvaluates.Where(s => s.StoreId == se1.StoreId).Select(s => s.Rating);
                int storeCount = storeRating.Count();
                double avgRating = 0;
                foreach(int s in storeRating)
                {
                    avgRating += s;
                }
                avgRating /= storeCount;
                var store = _db.Stores.Where(s => s.StoreId == se1.StoreId).FirstOrDefault();
                store.Rating = Convert.ToInt32(avgRating);
                _db.Stores.Update(store);
                _db.SaveChanges();

                return Json(new { type = 2 });
                //return RedirectToAction("Restaurant", "Store", new { id = se.StoreId });
            }// 有評論的場合:修改
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
        public async Task<IActionResult> Reserve([FromBody] StoreReserved sr)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);// 取得[登入會員]

            var s = _db.Stores.Where(s => s.StoreId == sr.StoreId).Select(st => st).FirstOrDefault();// 取得[該店家]
            var sr1 = _db.StoreReserveds.Where(s => s.StoreId == sr.StoreId && s.Date == sr.Date).Select(s => s);// 取得[該店家][當天]訂單 
            var gsr1 = sr1.GroupBy(x => x.Time, y => y.NumOfPeople, (time, num) => new // 以[時間]分組，每組輸出{時間，訂位人數總和}
            {
                Time = time,
                Sum = num.Sum()
            });
            var sumResult = gsr1.Where(g => g.Time == sr.Time).FirstOrDefault()?.Sum;
            if (sumResult >= s.Seats)
            {
                return Json(new { success = "false", errorType = 1 });
            }// 如果[訂位人數總和] >= [容客量]，跳出視窗：該時段已客滿
            else if ((sumResult + sr.NumOfPeople) > s.Seats)
            {
                return Json(new { success = "false", errorType = 2, numRemain = (s.Seats - sumResult) });
            }// 如果[訂位人數總和+欲訂位人數] > [容客量]，跳出視窗：選擇人數已超過容客量
            else
            {
                _db.StoreReserveds.Add(sr);
                _db.SaveChanges();
                return Json(new { success = "true" });
            }// 如果該時段有空位，存入資料庫並傳回success=true
        }
        [HttpPost]
        public async Task<IActionResult> IfSeatsOver([FromBody] StoreReserved sr)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);// 取得[登入會員]

            var s = _db.Stores.Where(s => s.StoreId == sr.StoreId).Select(st => st).FirstOrDefault();// 取得[該店家]
            var sr1 = _db.StoreReserveds.Where(s => s.StoreId == sr.StoreId && s.Date == sr.Date).Select(s => s);// 取得[該店家][當天]訂單 
            var gsr1 = sr1.GroupBy(x => x.Time, y => y.NumOfPeople, (time, num) => new // 以[時間]分組，每組輸出{時間，訂位人數總和}
            {
                Time = time,
                Sum = num.Sum()
            });
            var sumResult = gsr1.Where(g => g.Time == sr.Time).FirstOrDefault()?.Sum ?? 0;
            if (sumResult >= s.Seats)
            {
                return Json(new { success = "false", errorType = 1, numRemain = (s.Seats - (int)sumResult) });
            }// 如果[訂位人數總和] >= [容客量]，跳出視窗：該時段已客滿
            else
            {
                return Json(new { success = "true", errorType = 0, numRemain = (s.Seats - (int)sumResult) });
            }// 如果該時段有空位，傳回success=true
        }
        public IActionResult MyStoreReserved(int? id)
        {
            if (HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER) == null)
            {
                return RedirectToAction("Login", "User");
            }
            string json = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            Member mem = JsonSerializer.Deserialize<Member>(json);// 取得[登入會員]

            if (mem.MemberId == id)
            {
                var datas = from s in _db.StoreReserveds.Include(st => st.Store)
                            where s.MemberId == id
                            select s;
                //var mo = from m in _db.MealOrders.Include(st => st.Store)
                //         where m.MemberId == id 
                //         select m;
                return View(datas);
            }
            return RedirectToAction("memberManagement", "User");//如果傳進來的Id不是登入者的Id轉跳回會員頁面
        }
        public IActionResult LinkMealOrder(int? id)
        {
            var datas = _db.MealOrders.Include(mo => mo.Store).Where(mo => mo.StoreId == id).OrderBy(x=> Guid.NewGuid()).Select(t => new { t.OrderId, RestaurantName=t.Store.RestaurantName, t.Total, t.OrderDate, t.Remark }).First();
            return Json(datas);
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
                        .Count(),
                    newReview = _db.StoreEvaluates
                        .Where(se => se.StoreId == s.StoreId)
                        .OrderByDescending(se => se.EvaluateNo)
                        .Select(se => new
                        {
                            comments = se.Comments,
                            rating = se.Rating,
                            date = se.EvaluateDate.Value.ToString("yyyy-MM-dd"),
                            memberName = se.Member.MemberName
                        })
                        .FirstOrDefault()
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
            catch
            {
                return RedirectToAction("Error", "Home");
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
    }
}
