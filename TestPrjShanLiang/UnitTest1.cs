using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using prjShanLiang.Controllers;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;
using System.Runtime.CompilerServices;
using System.Text;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Data;
using System.Linq;

namespace TestPrjShanLiang
{
    [TestFixture]
    public class TestsStoreController
    {
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;
        StoreController _sc;

        public TestsStoreController()
        {
            _mockHttpContext = new();
            _mockSession = new();

            ShanLiang21Context db = new();
            _sc = new(db);

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _sc.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [Test]
        public void TestShowList()
        {
            var result = _sc.List();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestShowRestaurant()
        {
            int storeID = 1;
            IActionResult result = _sc.Restaurant(storeID);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetRestaurantType()
        {
            IActionResult result = _sc.GetRestaurantType(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchRestaurantType()
        {
            IActionResult result = _sc.SearchRestaurantType(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestShowFavorate()
        {
            IActionResult result = _sc.ShowFavorate(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestAddToFavorate()
        {
            IActionResult result = _sc.AddToFavorate(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestAddComment()
        {
            int id = 1;
            IActionResult result1 = _sc.AddComment(id);
            Assert.IsNotNull(result1);

            StoreEvaluate se = new();
            IActionResult result2 = _sc.AddComment(se);
            Assert.IsNotNull(result2);
        }

        [Test]
        public void TestReserve()
        {
            int id = 1;
            IActionResult result1 = _sc.Reserve(id);
            Assert.IsNotNull(result1);

            StoreReserved testData = new();
            var result2 = _sc.Reserve(testData);
            Assert.IsNotNull(result2);
        }

        [Test]
        public void TestGetName()
        {
            string keyword = "IJIDJh7e674841IOIG";
            IActionResult result = _sc.GetName(keyword);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetType()
        {
            IActionResult result = _sc.GetType();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestSearchStore()
        {
            IActionResult result0 = _sc.SearchStore(null, null, null, null);
            Assert.IsNotNull(result0);

            string keywoed = "膳糧";
            string types = "4";
            string districts = "[\"5\"]";
            int rating = 3;
            int order = 0;
            IActionResult result1 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result1);

            keywoed = "膳糧";
            types = "[\"4\"]";
            districts = "5";
            IActionResult result2 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result2);

            keywoed = "膳糧";
            types = "[\"4\"]";
            districts = "[\"5\"]";
            rating = 5;
            order = 1;
            IActionResult result2_2 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result2_2);

            order = 2;
            IActionResult result3 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result3);

            order = 3;
            IActionResult result4 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result4);

            order = 4;
            IActionResult result5 = _sc.SearchStore(keywoed, types, districts, rating, order);
            Assert.IsNotNull(result5);
        }

        [Test]
        public void TestGetRACAD()
        {
            object result = _sc.GetRACAD();
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsUserController
    {
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;
        Mock<IWebHostEnvironment>? _mockEnvironment;
        UserController _uc;

        public TestsUserController()
        {
            _mockHttpContext = new();
            _mockSession = new();
            _mockEnvironment = new();

            UserController uc = new(_mockEnvironment.Object);
            _uc = uc;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _uc.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [Test]
        public void TestLogin()
        {
            IActionResult result0 = _uc.Login();
            Assert.IsNotNull(result0);

            CAccountPasswordViewModel vm = new()
            {
                AccountName = "chris@gmail.com",
                AccountPassword = "aaaBBB1234567A",
            };
            IActionResult result1 = _uc.Login(vm);
            Assert.IsNotNull(result1);

            vm.AccountName = "ShanLiangBentou";
            vm.AccountPassword = "1234";
            IActionResult result2 = _uc.Login(vm);
            Assert.IsNotNull(result2);

            vm.AccountName = "Mark";
            vm.AccountPassword = "1234";
            IActionResult result3 = _uc.Login(vm);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestlogOut()
        {
            IActionResult result1 = _uc.logOut();
            Assert.IsNotNull(result1);
        }
        [Test]
        public void TestMemberManager()
        {
            string? account = null;
            IActionResult result1 = _uc.MemberManager(account);
            Assert.IsNotNull(result1);

            account = "NotFindMember@mail.com";
            IActionResult result2 = _uc.MemberManager(account);
            Assert.IsNotNull(result2);

            account = "Adam";
            IActionResult result3 = _uc.MemberManager(account);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestStoreManager()
        {
            string? account = null;
            IActionResult result1 = _uc.StoreManager(account);
            Assert.IsNotNull(result1);

            account = "NotFindStore";
            IActionResult result2 = _uc.StoreManager(account);
            Assert.IsNotNull(result2);

            account = "ShanLiangBentou";
            IActionResult result3 = _uc.StoreManager(account);
            Assert.IsNotNull(result3);
        }
        [Test]
        public void TestMypage()
        {
            IActionResult result1 = _uc.Mypage();
            Assert.IsNotNull(result1);
        }
        [Test]
        public void TestEdit()
        {
            string? account = null;
            IActionResult result1 = _uc.Edit(account);
            Assert.IsNotNull(result1);

            account = "admin";
            IActionResult result2 = _uc.Edit(account);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestSignup()
        {
            ShanLiang21Context db = new();

            //檢查註冊頁面是否正常跳轉
            IActionResult result1 = _uc.Signup();
            Assert.IsNotNull(result1);

            //新增一筆測試資料
            CCreateMemberAccountViewModel vm = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "測試市單元區會員里",
                AccountPassword = "TestPassword",
                AccountPassword2= "TestPassword"
            };
            //將資料帶入Signup方法中，建立一筆測試資料
            IActionResult result2 = _uc.Signup(vm);
            Assert.IsNotNull(result2);
            //檢查是否建立成功
            bool result3 = db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "測試市單元區會員里").Any();
            Assert.IsTrue(result3);

            //刪除測試資料
            db.Members.Remove(db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "測試市單元區會員里").First());
            db.SaveChanges();
            //檢查是否已刪除
            bool result4 = db.Members.Where(x => x.AccountName == "TestMember" && x.Address == "測試市單元區會員里").Any();
            Assert.IsFalse(result4);
        }
        [Test]
        public void TestSignupStore()
        {
            ShanLiang21Context db = new();

            IActionResult result1 = _uc.SignupStore();
            Assert.IsNotNull(result1);

            ViewResult? result2 = _uc.SignupStore() as ViewResult;
            Assert.IsNotNull(result2);

            Assert.IsNotNull(result2.ViewData["chosenCity"]);
            string chosenCity = result2.ViewData["chosenCity"].ToString();
            Assert.AreEqual("", chosenCity);

            Assert.IsNotNull(result2.ViewData["chosenDistrict"]);
            string chosenDistrict = result2.ViewData["chosenDistrict"].ToString();
            Assert.AreEqual("", chosenDistrict);

            CCreateStoreAccountViewModel vm = new()
            {
                AccountName = "TestStore",
                TaxID = "00000000",
                RestaurantName = "TestStoreName",
                RestaurantAddress = "測試市單元區店家里",
                RestaurantPhone = "(02)0000-0000",
                Website = "www.TestStore.com",
                Seats = 20,
                StoreMail = "TsetStore@mail",
                AccountPassword = "TestPassword",
                storeDistrict = "大安區",
            };
            List<IFormFile> files = new();
            Store vm2 = new()
            {
                OpeningTime = TimeSpan.FromHours(0),
                ClosingTime = TimeSpan.FromHours(10),
            };
            IActionResult result3 = _uc.SignupStore(vm, files, vm2);
            Assert.IsNotNull(result3);
            //檢查是否建立成功
            bool result4 = db.Stores.Where(x => x.AccountName == "TestStore" && x.RestaurantAddress == "測試市單元區店家里").Any();
            Assert.IsTrue(result4);

            //刪除測試資料
            var datas = db.Stores.Where(x => x.AccountName == "TestStore");
            foreach (var data in datas)
            {
                db.Stores.Remove(data);
            }
            //db.Stores.Remove(db.Stores.Where(x => x.AccountName == "TestStore").First());
            db.SaveChanges();
            //檢查是否已刪除
            bool result5 = db.Stores.Where(x => x.AccountName == "TestStore" && x.RestaurantAddress == "測試市單元區店家里").Any();
            Assert.IsFalse(result5);
        }
        [Test]
        public void TestMemberManagement()
        {
            //IActionResult result = _uc.memberManagement();
            //Assert.IsNotNull(result);
        }
        [Test]
        public void TestStoreManagement()
        {
            //IActionResult result = _uc.storeManagement();
            //Assert.IsNotNull(result);
        }
        [Test]
        public void TestMemberDataRevision()
        {
            ShanLiang21Context db = new();

            string email = "";
            IActionResult result1 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result1);

            Member testData = new()
            {
                Email = "Test@mail",
                Password = "password",
                CustomerLevel = 0
            };
            db.Members.Add(testData);
            db.SaveChanges();

            email = "Test@mail";
            bool result2 = db.Members.Where(x => x.Email == email).Any();
            Assert.IsTrue(result2);

            IActionResult result3 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result3);
            ViewResult result3_2 = result3 as ViewResult;
            Assert.IsNotNull(result3_2.ViewData["CustomerLevel"]);
            string CustomerLevel = result3_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("一般會員"));

            testData.CustomerLevel = 1;
            db.Members.Update(testData);
            db.SaveChanges();
            IActionResult result4 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result4);
            ViewResult result4_2 = result3 as ViewResult;
            Assert.IsNotNull(result4_2.ViewData["CustomerLevel"]);
            CustomerLevel = result4_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("白金會員"));

            testData.CustomerLevel = 2;
            db.Members.Update(testData);
            db.SaveChanges();
            IActionResult result5 = _uc.memberDataRevision(email);
            Assert.IsNotNull(result5);
            ViewResult result5_2 = result3 as ViewResult;
            Assert.IsNotNull(result5_2.ViewData["CustomerLevel"]);
            CustomerLevel = result5_2.ViewData["CustomerLevel"].ToString();
            Assert.That(CustomerLevel, Is.EqualTo("鑽石會員"));

            db.Members.Remove(testData);
            db.SaveChanges();
        }
        [Test]
        public void TestStoreDataRevision()
        {
            string account = "";
            IActionResult result1 = _uc.storeDataRevision(account);
            Assert.IsNotNull(result1);

            account = "ShanLiangBentou";
            IActionResult result2 = _uc.storeDataRevision(account);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestDelete()
        {
            string account = "";
            IActionResult result = _uc.Delete(account);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestMemberDataRevision2()
        {
            ShanLiang21Context db = new();
            //新增一筆測試資料
            Member testData = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "測試市單元區會員里",
                Password = "TestPassword",
            };
            db.Members.Add(testData);
            db.SaveChanges();
            string Address = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Address).First();
            //確認其他資料是否為更改前的
            Assert.That(Address, Is.EqualTo("測試市單元區會員里"));

            //新增一筆用來更改的資料
            CMemberWrap vm = new()
            {
                Email = "Test@email.com",
                Memberphone = "0988888888",
                MemberName = "EditName",
                Address = "測試市單元區編輯里"
            };
            //測試不包含更改密碼
            IActionResult result1 = _uc.memberDataRevision2(vm);
            Assert.IsNotNull(result1);
            Address = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Address).First();
            //確認其他資料是否已經更改
            Assert.That(Address, Is.EqualTo("測試市單元區編輯里"));
            //確認密碼是否為更改前的
            string Password = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).First();
            Assert.That(Password, Is.EqualTo("TestPassword"));

            //測試包含更改密碼
            vm.Password = "EditPassword";
            IActionResult result2 = _uc.memberDataRevision2(vm);
            Assert.IsNotNull(result2);
            Password = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).First();
            //確認密碼是否是已經更改
            Assert.That(Password, Is.EqualTo("EditPassword"));

            //刪除測試資料
            db.Members.Remove(db.Members.Where(x => x.Email == "Test@email.com").First());
            db.SaveChanges();
        }   
        [Test]
        public void TestStoreDataRevision2()
        {
            //ShanLiang21Context db = new();
            //List<IFormFile> files = new();
            ////新增一筆測試資料
            //Store testData = new()
            //{
            //    AccountName = "TestStore",
            //    DistrictId = 1,
            //    TaxId = "00000000",
            //    RestaurantName = "TestStoreName",
            //    RestaurantAddress = "測試市單元區店家里",
            //    RestaurantPhone = "(02)0000-0000",
            //    Seats = 20,
            //    OpeningTime = TimeSpan.FromHours(0),
            //    ClosingTime = TimeSpan.FromHours(10),
            //    Website = "www.TestStore.com",
            //    StoreMail = "TsetStore@mail",
            //    Password = "TestPassword"
            //};
            //db.Stores.Add(testData);
            //db.SaveChanges();
            //string Address = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.RestaurantAddress).First();
            ////確認其他資料是否為更改前的
            //Assert.That(Address, Is.EqualTo("測試市單元區店家里"));

            ////新增一筆更改的資料
            //Store editData = new()
            //{
            //    AccountName = "EditStore",
            //    TaxId = "88888888",
            //    RestaurantName = "EditStoreName",
            //    RestaurantAddress = "測試市單元區編輯里",
            //    RestaurantPhone = "(02)8888-8888",
            //    Seats = 10,
            //    OpeningTime = TimeSpan.FromHours(1),
            //    ClosingTime = TimeSpan.FromHours(9),
            //    Website = "www.EditStore.com",
            //    StoreMail = "EditStore@mail"
            //};
            //IActionResult result1 = _uc.storeDataRevision2(testData, files);
            //Assert.IsNotNull(result1);
            //Address = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.RestaurantAddress).First();
            ////確認其他資料是否已經更改
            //Assert.That(Address, Is.EqualTo("測試市單元區編輯里"));
            //string Password = db.Stores.Where(x => x.AccountName == "TestStore").Select(x => x.Password).First();
            ////確認密碼是否為更改前的
            //Assert.That(Password, Is.EqualTo("TestPassword"));

            //editData.Password = "EditPassword";

            //IActionResult result2 = _uc.storeDataRevision2(testData, files);
            //Assert.IsNotNull(result2);
            ////確認密碼是否為已經更改
            //Assert.That(Password, Is.EqualTo("EditPassword"));

            ////刪除測試資料
            //db.Stores.Remove(db.Stores.Where(x => x.AccountName == "TestStore").First());
            //db.SaveChanges();
        }
        [Test]
        public void TestGetCities()
        {
            IActionResult result = _uc.GetCities();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestGetDistricts()
        {
            string cityName = "台北市";
            IActionResult result = _uc.GetDistricts(cityName);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckName()
        {
            string name = "NoFindMember";
            IActionResult result = _uc.CheckName(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckStoreName()
        {
            string name = "NoFindStore";
            IActionResult result = _uc.CheckStoreName(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCheckLoginAccount()
        {
            string name = "NoFindAll";
            IActionResult result = _uc.CheckLoginAccount(name);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestForgetPwd()
        {
            IActionResult result = _uc.forgetPwd();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestSendEmail()
        {
            ShanLiang21Context db = new();
            //新增一筆測試資料
            Member testData = new()
            {
                AccountName = "TestMember",
                Memberphone = "TestPhone",
                MemberName = "TestName",
                Email = "Test@email.com",
                BrithDate = DateTime.Now,
                Address = "測試市單元區會員里",
                Password = "TestPassword",
            };
            db.Members.Add(testData);
            db.SaveChanges();

            IActionResult result1 = _uc.sendEmail("Test@email.com");
            Assert.IsNotNull(result1);
            //確認密碼是否已經被更改
            bool isEditPwd = db.Members.Where(x => x.Email == "Test@email.com").Select(x => x.Password).Equals("TestPassword");
            Assert.IsFalse(isEditPwd);

            //刪除測試資料
            db.Members.Remove(db.Members.Where(x => x.Email == "Test@email.com").First());
            db.SaveChanges();
        }
    }
    [TestFixture]
    public class TestsUserAdminController
    {
        UserAdminController _uac;
        public TestsUserAdminController()
        {
            UserAdminController uac = new();
            _uac = uac;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _uac.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _uac.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestEdit()
        {
            int? TestMemberID = null;
            IActionResult result1 = _uac.Edit(TestMemberID);
            Assert.IsNotNull(result1);
            ShanLiang21Context db = new();
            Member mem = new()
            {
                Email = "TestMember@mail.com",
                Password = "0000"
            };
            db.Add(mem);
            db.SaveChanges();
            TestMemberID = db.Members.Where(x => x.Email == "TestMember@mail.com").Select(x => x.MemberId).First();
            IActionResult result2 = _uac.Edit(TestMemberID);
            Assert.IsNotNull(result2);
            Member memEdit = new()
            {
                MemberId = (int)TestMemberID,
                Email = "TestEdit@mail.com",
                MemberName = "Member",
                Memberphone = "0900000000",
                BrithDate = DateTime.Now,
                Address = "測試市單元區編輯里",
                AccountStatus = 1
            };
            IActionResult result3 = _uac.Edit(memEdit);
            Assert.IsNotNull(result3);
            Member d = db.Members.FirstOrDefault(x => x.MemberId == TestMemberID);
            db.Members.Remove(d);
            db.SaveChanges();
        }
        [Test]
        public void TestCheckStatus()
        {
            ShanLiang21Context db = new();
            int L = db.AccountStatuses.Count();
            for(int statusID = 0; statusID < L + 1; statusID++)
            {
                IActionResult result = _uac.CheckStatus(1);
                Assert.IsNotNull(result);
            }
        }
    }
    [TestFixture]
    public class TestsStoreAdminController
    {
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;
        Mock<IWebHostEnvironment>? _mockEnvironment;
        Mock<IEmailSender>? _mockEmailSender;
        StoreAdminController _sac;
        public TestsStoreAdminController()
        {
            _mockHttpContext = new();
            _mockSession = new();
            _mockEnvironment = new();
            _mockEmailSender = new();

            StoreAdminController sac = new(_mockEnvironment.Object, _mockEmailSender.Object);
            _sac = sac;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _sac.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
            
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _sac.List();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestEdit()
        {
            ShanLiang21Context db = new();
            int id = 99999999;

            IActionResult result1 = _sac.Edit(id);
            Assert.IsNotNull(result1);

            id = 1;
            IActionResult result2 = _sac.Edit(id);
            Assert.IsNotNull(result2);

            //新增一筆測試用資料
            Store testData = new()
            {
                AccountName = "TestStore",
                RestaurantName = "TestStoreName",
                TaxId = "00000000",
                RestaurantPhone = "(02)0000-0000",
                RestaurantAddress = "測試市單元區店家里",
                StoreMail = "TsetStore@mail",
                Website = "www.TestStore.com",
                Seats = 20,
                OpeningTime = TimeSpan.FromHours(0),
                ClosingTime = TimeSpan.FromHours(10),
                Password = "TestPassword",
                DistrictId = 1
            };
            db.Stores.Add(testData);
            db.SaveChanges();

            int storeID = db.Stores.Where(x => x.AccountName == "TestStore").First().StoreId;
            string name = db.Stores.Where(x => x.StoreId == storeID).First().RestaurantName;
            Assert.That(name, Is.EqualTo("TestStoreName"));

            //新增一筆修改用資料
            Store editData = new()
            {
                StoreId = storeID,
                AccountName = "EditStore",
                RestaurantName = "EditStoreName",
                TaxId = "88888888",
                RestaurantPhone = "(02)8888-8888",
                RestaurantAddress = "測試市單元區編輯里",
                StoreMail = "EditStore@mail",
                Website = "www.EditStore.com",
                Seats = 10,
                OpeningTime = TimeSpan.FromHours(1),
                ClosingTime = TimeSpan.FromHours(9),
                Password = "EditPassword",
                DistrictId = 2
            };
            IActionResult result3 = _sac.Edit(editData, null) ;
            Assert.IsNotNull(result3);
            //name = db.Stores.Where(x => x.StoreId == storeID).First().RestaurantName;
            //Assert.That(name, Is.EqualTo("EditStoreName"));

            db.Stores.Remove(db.Stores.Where(x => x.StoreId == storeID).First());
            db.SaveChanges();
        }

        [Test]
        public void TestVerify()
        {
            ShanLiang21Context db = new();
            Store testData = new()
            {
                AccountName = "TestStore",
                RestaurantName = "TestStoreName",
                TaxId = "00000000",
                RestaurantPhone = "(02)0000-0000",
                RestaurantAddress = "測試市單元區店家里",
                StoreMail = "TsetStore@mail",
                Website = "www.TestStore.com",
                Seats = 20,
                OpeningTime = TimeSpan.FromHours(0),
                ClosingTime = TimeSpan.FromHours(10),
                Password = "TestPassword",
                DistrictId = 1,
                AccountStatus = 0
            };
            db.Stores.Add(testData);
            db.SaveChanges();

            int storeID = db.Stores.Where(x => x.AccountName == "TestStore").First().StoreId;

            IActionResult result = _sac.Verify(storeID);
            Assert.IsNotNull(result);

            db.Stores.Remove(db.Stores.Where(x => x.StoreId == storeID).First());
            db.SaveChanges();
        }

        [Test]
        public void TestCompleteVerification()
        {
            ShanLiang21Context db = new();
            Store testData = new()
            {
                AccountName = "TestStore",
                RestaurantName = "TestStoreName",
                TaxId = "00000000",
                RestaurantPhone = "(02)0000-0000",
                RestaurantAddress = "測試市單元區店家里",
                StoreMail = "TsetStore@mail",
                Website = "www.TestStore.com",
                Seats = 20,
                OpeningTime = TimeSpan.FromHours(0),
                ClosingTime = TimeSpan.FromHours(10),
                Password = "TestPassword",
                DistrictId = 1,
                AccountStatus = 0
            };
            db.Stores.Add(testData);
            db.SaveChanges();

            int storeID = db.Stores.Where(x => x.AccountName == "TestStore").First().StoreId;

            IActionResult result1 = _sac.CompleteVerification(storeID);
            Assert.IsNotNull(result1);

            //int? storeStatus = db.Stores.Where(x => x.StoreId == storeID).First().AccountStatus;
            //Assert.That(storeStatus, Is.EqualTo((int?)1));

            IActionResult result2 = _sac.CompleteVerification(storeID);
            Assert.IsNotNull(result2);

            db.Stores.Remove(db.Stores.Where(x => x.StoreId == storeID).First());
            db.SaveChanges();
        }
    }
    [TestFixture]
    public class TestsShoppingController
    {
        Mock<IWebHostEnvironment> _mockEnvironment;
        Mock<HttpContext> _mockHttpContext;
        Mock<ISession> _mockSession;
        ShoppingController _sc;
        public TestsShoppingController()
        {
            _mockEnvironment = new();
            _mockHttpContext = new();
            _mockSession = new();

            ShanLiang21Context db = new();
            ShoppingController sc = new(db,_mockEnvironment.Object);
            _sc = sc;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _sc.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }

        [Test]
        public void TestMenu()
        {
            ShanLiang21Context db = new();
            int id = 1;

            IActionResult result1 = _sc.Menu(id);
            Assert.IsNotNull(result1);

            var store = db.Stores.Where(x => x.StoreId == id).Select(x => new { x.RestaurantAddress, x.RestaurantName, x.RestaurantPhone}).First();
            string storeAddress = store.RestaurantAddress;
            string storeName = store.RestaurantName;
            string storePhone = store.RestaurantPhone;

            ViewResult result2 = result1 as ViewResult;
            Assert.IsNotNull(result2.ViewData["RestaurantAddress"]);
            string RestaurantAddress = result2.ViewData["RestaurantAddress"].ToString();
            Assert.IsNotNull(result2.ViewData["RestaurantName"]);
            string AccountName = result2.ViewData["RestaurantName"].ToString();
            Assert.IsNotNull(result2.ViewData["RestaurantPhone"]);
            string RestaurantPhone = result2.ViewData["RestaurantPhone"].ToString();
            Assert.That(RestaurantAddress, Is.EqualTo(storeAddress));
            Assert.That(AccountName, Is.EqualTo(storeName));
            Assert.That(RestaurantPhone, Is.EqualTo(storePhone));
        }

        [Test]
        public void TestShowCartCount()
        {
            IActionResult result = _sc.ShowCartCount();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestAddToCart()
        {
            IActionResult result = _sc.AddToCart(1, 10);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestCartView()
        {
            IActionResult result = _sc.CartView();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestDelete()
        {
            int? id = null;
            IActionResult result = _sc.Delete(id);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestCheckoutCart()
        {
            IActionResult result = _sc.CheckoutCart();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestCreateOrder()
        {
            int pay = 1;
            string remark = "";
            int paymethod = 1;
            IActionResult result = _sc.CreateOrder(pay, remark, paymethod);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestMyMealOrder()
        {
            IActionResult result = _sc.MyMealOrder(33);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestMyMealOrderDetail()
        {
            IActionResult result = _sc.MyMealOrderDetail(33);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestStoreMenuList()
        {
            IActionResult result = _sc.StoreMenuList();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestMealOrderList()
        {
            IActionResult result = _sc.MealOrderList();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestCheckOrder()
        {
            int? orderID = null;
            int? status = null;
            IActionResult result1 = _sc.CheckOrder(orderID, status);
            Assert.IsNotNull(result1);

            orderID = 999999999;
            IActionResult result2 = _sc.CheckOrder(orderID, status);
            Assert.IsNotNull(result2);

            orderID = 58;
            status = 9;
            IActionResult result3 = _sc.CheckOrder(orderID, status);
            Assert.IsNotNull(result3);
        }

        [Test]
        public void TestCreateMenu()
        {
            ShanLiang21Context db = new();

            //IActionResult result1 = _sc.CreateMenu();
            //Assert.IsNotNull(result1);

            CMealViewModel testData = new()
            {
                MealName = "TestMeal",
                MealPrice = 100,
                Recommendation = "TestRecommendation",
                StoreId = 1,
            };
            IActionResult result2 = _sc.CreateMenu(testData);
            Assert.IsNotNull(result2);
            int mealID = db.MealMenus.Where(x => x.MealName == "TestMeal").First().MealId;
            bool isCreate = db.MealMenus.Where(x => x.MealId == mealID).Any();
            Assert.IsTrue(isCreate);

            db.MealMenus.Remove(db.MealMenus.Where(x => x.MealId == mealID).First());
            db.SaveChanges();
        }

        [Test]
        public void TestEditMenu()
        {
            ShanLiang21Context db = new();

            MealMenu testData = new()
            {
                MealName = "TestMeal",
                MealPrice = 100,
                Recommendation = "TestRecommendation",
                StoreId = 1,
            };
            db.MealMenus.Add(testData);
            db.SaveChanges();
            int mealID = 999999;
            IActionResult result1 = _sc.EditMenu(mealID);
            Assert.IsNotNull(result1);

            mealID = db.MealMenus.Where(x => x.MealName == "TestMeal").First().MealId;
            IActionResult result2 = _sc.EditMenu(mealID);
            Assert.IsNotNull(result2);

            CMealViewModel editData = new()
            {
                MealId = mealID,
                MealName = "EditMeal",
                MealPrice = 200,
                Recommendation = "EditRecommendation"
            };
            IActionResult result3 = _sc.EditMenu(editData);
            Assert.IsNotNull(result3);

            //string name = db.MealMenus.Where(x => x.MealId == mealID).First().MealName;
            //Assert.That(name, Is.EqualTo("EditMeal"));

            db.MealMenus.Remove(db.MealMenus.Where(x => x.MealId == mealID).First());
            db.SaveChanges();
        }

        [Test]
        public void TestDeleteMenu()
        {
            ShanLiang21Context db = new();

            MealMenu testData = new()
            {
                MealName = "TestMeal",
                MealPrice = 100,
                Recommendation = "TestRecommendation",
                StoreId = 1,
            };
            db.MealMenus.Add(testData);
            db.SaveChanges();

            int mealID = db.MealMenus.Where(x => x.MealName == "TestMeal").First().MealId;

            IActionResult result = _sc.DeleteMenu(mealID);
            Assert.IsNotNull(result);
            bool isDelete = db.MealMenus.Where(x => x.MealId == mealID).Any();
            Assert.IsFalse(isDelete);
        }
    }
    [TestFixture]
    public class TestsHomeController
    {
        HomeController _hc;
        public TestsHomeController()
        {
            ShanLiang21Context db = new();
            HomeController hc = new(db);
            _hc = hc;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _hc.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestPrivacy()
        {
            IActionResult result = _hc.Privacy();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestError()
        {
            IActionResult result = _hc.Error();
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestChartAdminController
    {
        ChartAdminController _cac;
        public TestChartAdminController()
        {
            ShanLiang21Context db = new();
            ChartAdminController cac = new(db);
            _cac = cac;
        }

        [Test]
        public void TestIndex()
        {
            IActionResult result = _cac.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetStoreTypeCount()
        {
            IActionResult result = _cac.GetStoreTypeCount();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestGetStorePocketCount()
        {
            IActionResult result = _cac.GetStorePocketCount();
            Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsBloggerController
    {
        BloggerController _bc;
        public TestsBloggerController()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            BloggerController bc = new(mockEnvironment.Object);
            _bc = bc;
        }
        [Test]
        public void TestBloggerList()
        {
            CKeywordViewModel vm = new()
            {
                txtKeyword = ""
            };
            IActionResult result1 = _bc.BloggerList(vm);
            Assert.IsNotNull(result1);

            vm.txtKeyword = "日式";
            IActionResult result2 = _bc.BloggerList(vm);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestBloggerCard()
        {
            CKeywordViewModel vm = new()
            {
                txtKeyword = ""
            };
            IActionResult result1 = _bc.BloggerCard(vm);
            Assert.IsNotNull(result1);

            vm.txtKeyword = "日式";
            IActionResult result2 = _bc.BloggerList(vm);
            Assert.IsNotNull(result2);
        }
        [Test]
        public void TestBloggerCED()
        {
            //新增
            ShanLiang21Context db = new();
            IActionResult result1 = _bc.BloggerCreate();
            Assert.IsNotNull(result1);
            int BlogID;
            Blog blog = new(){
                BlogHeader = "TestHeader",
                BlogContent = "這是一個單元測試，你不應該看到這篇文章。",
                CityName = "測試市",
                DistrictName = "單元區",
                RestaurantName = "TestCoffee"
            };
            IActionResult result2 = _bc.BloggerCreate(blog);
            Assert.IsNotNull(result2);
            //修改

            BlogID = db.Blogs.Where(b => b.BlogHeader == "TestHeader").Select(b => b.BlogId).First();
            IActionResult result3 = _bc.BloggerEdit(BlogID);
            Assert.IsNotNull(result3);
            IActionResult result4 = _bc.BloggerEdit(999999999);
            Assert.IsNotNull(result4);

            CBlogwrap edit = new()
            {
                blog = blog,
                BlogId = BlogID,
                BlogHeader = "TestHeaderEdit",
                BlogContent = "這是一個單元測試後的編輯，你還是不該看到這篇文章。",
                //photo = @"eeee/eee",
            };
            IActionResult result5 = _bc.BloggerEdit(edit);
            Assert.IsNotNull(result5);

            //刪除
            IActionResult result6 = _bc.BloggerDelete(BlogID);
            Assert.IsNotNull(result6);
            IActionResult result7 = _bc.BloggerDelete(999999999);
            Assert.IsNotNull(result7);
        }
        [Test]
        public void TestBloggerDetail()
        {
            IActionResult result1 = _bc.BloggerDetail(999999999);
            Assert.IsNotNull(result1);
            IActionResult result2 = _bc.BloggerDetail(1);
            Assert.IsNotNull(result2);
        }
    }
    [TestFixture]
    public class TestsAdvertisementController
    {
        AdvertisementController _ac;
        Mock<HttpContext>? _mockHttpContext;
        Mock<ISession>? _mockSession;

        public TestsAdvertisementController()
        {
            Mock<HttpContext> mockHttpContext = new();
            _mockHttpContext = mockHttpContext;

            Mock<ISession> mockSession = new();
            _mockSession = mockSession;

            ShanLiang21Context db = new();
            AdvertisementController ac = new(db);
            _ac = ac;

            _mockHttpContext.SetupGet(x => x.Session).Returns(_mockSession.Object);

            _ac.ControllerContext = new()
            {
                HttpContext = _mockHttpContext.Object
            };
        }
        [Test]
        public void TestBuyAdv()
        {
            IActionResult result = _ac.BuyAdv();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestAddAdvToCart()
        {
            int? id = null;
            IActionResult result1 = _ac.AddAdvToCart(id);
            Assert.IsNotNull(result1);

            id = 1;
            IActionResult result2 = _ac.AddAdvToCart(id);
            Assert.IsNotNull(result2);

            ViewResult result3 = _ac.AddAdvToCart(id) as ViewResult;
            string message = "";
            foreach (var c in result3.ViewData.Values)
            {
                message += c.ToString();
            }
            Assert.AreEqual("1", message);

            CAddAdvToCartViewModel vm = new()
            {
                txtCount = 1,
                txtStoreID = 1
            };
            IActionResult result4 = _ac.AddAdvToCart(vm);
            Assert.IsNotNull(result4);
        }
        [Test]
        public void TestDelete()
        {
            int? id = null;
            IActionResult result1 = _ac.Delete(id);
            Assert.IsNotNull(result1);

            //id = 1;
            //IActionResult result2 = _ac.Delete(id);
            //Assert.IsNotNull(result2);
        }
        [Test]
        public void TestCartView()
        {
            IActionResult result = _ac.CartView();
            Assert.IsNotNull(result);
        }
        [Test]
        public void ConfirmADOrder()
        {
            //IActionResult result = _ac.ConfirmADOrder();
            //Assert.IsNotNull(result);
        }
    }
    [TestFixture]
    public class TestsAdminController
    {
        AdminController _ac;
        public TestsAdminController()
        {
            AdminController ac = new();
            _ac = ac;
        }
        [Test]
        public void TestIndex()
        {
            IActionResult result = _ac.Index();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestList()
        {
            IActionResult result = _ac.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestCreate()
        {
            IActionResult result1 = _ac.Create();
            Assert.IsNotNull(result1);

            Admin a = new()
            {
                IdentificationId = 1
            };
            IActionResult result2 = _ac.Create(a);
            Assert.IsNotNull(result2);
            ViewResult result3 = _ac.Create(a) as ViewResult;
            string message = "";
            foreach(var c in result3.ViewData.Values)
            {
                message += c.ToString();
            }
            Assert.AreEqual("無法修改權限", message);

            a.IdentificationId = 3;
            IActionResult result4 = _ac.Create(a);
            Assert.IsNotNull(result4);
        }
        [Test]
        public void TestDelete()
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Admin a = new()
            {
                AdminName = "TestProjectAdmin",
                Passwoed = "0000",
                IdentificationId = 1
            };
            db.Add(a);
            db.SaveChanges();
            Assert.IsTrue(db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Any());
            int TestAdminID = db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Select(x => x.AdminId).First();
            IActionResult result = _ac.Delete(TestAdminID);
            Assert.IsFalse(db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Any());
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestEdit()
        {
            int TestAdminID = 999999999;
            IActionResult result1 = _ac.Edit(TestAdminID);
            Assert.IsNotNull(result1);

            ShanLiang21Context db = new ShanLiang21Context();
            Admin a = new()
            {
                AdminName = "TestProjectAdmin",
                Passwoed = "0000",
                IdentificationId = 1
            };
            db.Add(a);
            db.SaveChanges();
            TestAdminID = db.Admins.Where(x => x.AdminName == "TestProjectAdmin").Select(x => x.AdminId).First();
            IActionResult result2 = _ac.Edit(TestAdminID);
            Assert.IsNotNull(result2);
            a.AdminName = "TestProjectAdminEdit";
            a.Passwoed = "1111";
            a.IdentificationId = 3;
            IActionResult result3 = _ac.Edit(a);
            Assert.IsNotNull(result3);
            db.Admins.Remove(a);
            db.SaveChanges();
        }
    }
    [TestFixture]
    public class TestsZ
    {
        public TestsZ()
        {
            ShanLiang21Context db = new();
            var s = db.Stores.Where(x => x.AccountName == "TestStore" || x.AccountName == "EditStore");
            foreach (var item in s)
            {
                db.Stores.Remove(item);
            }
            var m = db.Members.Where(x => x.Email == "TestMember@mail.com" || x.Email == "EditMember@mail.com");
            foreach (var item in m)
            {
                db.Members.Remove(item);
            }
            var a = db.Accounts.Where(x => x.AccountName == "TestStore" || x.AccountName == "EditStore" || x.AccountName == "TestMember@mail.com" || x.AccountName == "EditMember@mail.com");
            foreach (var item in a)
            {
                db.Accounts.Remove(item);
            }
            db.SaveChanges();
        }
        [Test]
        public void Delete()
        {

        }
    }
}