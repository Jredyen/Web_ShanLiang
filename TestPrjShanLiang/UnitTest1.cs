using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prjShanLiang.Controllers;
using prjShanLiang.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestPrjShanLiang
{
    public class TestsStoreController
    {
        StoreController _sc;
        public TestsStoreController()
        {
            _sc = new();
        }
        [Test]
        public void TestStoreList()
        {
            var result = _sc.List();
            Assert.Null(result);
        }
        [Test]
        public void TestGetKeyword()
        {
            string keyword = "IJIDJh7e674841IOIG";
            IActionResult result = _sc.GetStore(keyword);
            Assert.Null(result);
        }
        [Test]
        public void TestGetType()
        {
            IActionResult result = _sc.ShowType();
            Assert.Null(result);
        }
        [Test]
        public void TestSearchHaveKeyword()
        {
            string keywoed = "¿¯Â³";
            int[]? types = { 1, 2, 3, 4, 5 };
            int[]? districts = { 1, 2, 3, 4, 5 };
            int rating = 3;
            IActionResult result = _sc.SearchStore(keywoed, types, districts, rating);
            Assert.Null(result);
        }
        [Test]
        public void TestSearchNoKeyword()
        {
            IActionResult result = _sc.SearchStore(null, null, null, null);
            Assert.Null(result);
        }
        [Test]
        public void TestShowRestaurant()
        {
            int storeID = 1;
            IActionResult result = _sc.Restaurant(storeID);
            Assert.Null(result);
        }
        [Test]
        public void TestNoInputRestaurantID()
        {
            IActionResult result = _sc.Restaurant(null);
            Assert.Null(result);
        }

    }
    public class TestsUserController
    {
        public TestsUserController()
        {

        }
        [Test]
        public void Test()
        {
            //Assert.Null();
        }
    }
}