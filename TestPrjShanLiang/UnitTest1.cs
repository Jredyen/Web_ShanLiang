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

namespace TestPrjShanLiang
{
    [TestFixture]
    public class TestsStoreController
    {
        StoreController _sc;
        public TestsStoreController()
        {
            ShanLiang21Context db = new();
            _sc = new(db);
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

        }
        //[Test]
        //public void TestShowFavorate()
        //{
        //    IActionResult result = _sc.ShowFavorate(1);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void TestAddComment()
        //{
        //    StoreEvaluate se = new();
        //    IActionResult result = _sc.AddComment(se);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void TestReserveInt()
        //{
        //    IActionResult result = _sc.Reserve(1);
        //    Assert.IsNotNull(result);
        //}
        //[Test]
        //public void TestReservePost()
        //{
        //    StoreEvaluate se = new();
        //    IActionResult result = _sc.Reserve(se);
        //    Assert.IsNotNull(result);
        //}

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
        //[Test]
        //public void TestSearchStoreNoKeyword()
        //{
        //    IActionResult result = _sc.SearchStore(null, null, null, null);
        //    Assert.IsNotNull(result);
        //}

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
        private UserController _controller;
        private Mock<IWebHostEnvironment> _mockEnvironment;
        private Mock<HttpContext> _mockHttpContext;
        private Mock<ISession> _mockSession;
        UserController _uc;
        public TestsUserController()
        {
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();
            _controller = new UserController(_mockEnvironment.Object);
            _controller.ControllerContext.HttpContext = _mockHttpContext.Object;
            _mockHttpContext.SetupGet(c => c.Session).Returns(_mockSession.Object);

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            UserController uc = new(mockEnvironment.Object);
            _uc = uc;
        }

        [Test]
        public void TestLogin()
        {
            //IActionResult result0 = _uc.Login();
            //Assert.IsNotNull(result0);

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
            
        }
        [Test]
        public void TestMemberManager()
        {

        }
        [Test]
        public void TestStoreManager()
        {

        }
        [Test]
        public void TestMypage()
        {

        }
        [Test]
        public void TestEdit()
        {

        }
        [Test]
        public void TestSignup()
        {

        }
        [Test]
        public void TestSignupStore()
        {

        }
        [Test]
        public void TestMemberManagement()
        {

        }
        [Test]
        public void TestStoreManagement()
        {

        }
        [Test]
        public void TestMemberDataRevision()
        {

        }
        [Test]
        public void TestGetCustomerLevel()
        {

        }
        [Test]
        public void TestStoreDataRevision()
        {

        }
        [Test]
        public void TestMemberDataRevision2()
        {

        }
        [Test]
        public void TestStoreDataRevision2()
        {

        }
        [Test]
        public void TestGetCities()
        {

        }
        [Test]
        public void TestGetDistricts()
        {

        }
        [Test]
        public void TestCheckName()
        {

        }
        [Test]
        public void TestCheckStoreName()
        {

        }
        [Test]
        public void TestCheckLoginAccount()
        {

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
        [Test]
        public void Test() { }
        [Test]
        public void Test2() { }
        [Test]
        public void Test3() { }
        [Test]
        public void Test4() { }
        [Test]
        public void Test5() { }
        [Test]
        public void Test6() { }
        [Test]
        public void Test7() { }
        [Test]
        public void Test8() { }
    }
    [TestFixture]
    public class TestsShoppingController
    {
        ShoppingController _sc;
        public TestsShoppingController()
        {
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            ShanLiang21Context db = new();
            ShoppingController sc = new(db,mockEnvironment.Object);
            _sc = sc;
        }
        [Test]
        public void TestMenu()
        {
            IActionResult result = _sc.Menu(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestMyMealOrderDetail()
        {
            IActionResult result = _sc.MyMealOrderDetail(33);
            Assert.IsNotNull(result);
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
        public void Test2() { }
        [Test]
        public void Test3() { }
        [Test]
        public void Test4() { }
        [Test]
        public void Test5() { }
        [Test]
        public void Test6() { }
        [Test]
        public void Test7() { }
    }
    [TestFixture]
    public class TestsAdvertisementController
    {
        AdvertisementController _ac;
        public TestsAdvertisementController()
        {
            ShanLiang21Context db = new();
            AdvertisementController ac = new(db);
            _ac = ac;
        }
        [Test]
        public void TestBuyAdv()
        {
            IActionResult result = _ac.BuyAdv();
            Assert.IsNotNull(result);
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

            a.IdentificationId = 3;
            IActionResult result3 = _ac.Create(a);
            Assert.IsNotNull(result3);
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
}