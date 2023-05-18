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
            ShanLiang21Context db = new();
            _sc = new(db);
        }
        [Test]
        public void TestStoreList()
        {
            var result = _sc.List();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestGetKeyword()
        {
            string keyword = "IJIDJh7e674841IOIG";
            IActionResult result = _sc.GetName(keyword);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestGetType()
        {
            IActionResult result = _sc.ShowType();
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestSearchHaveKeyword()
        {
            string keywoed = "¿¯Â³";
            string types = " 1 2, 3, 4, 5 ";
            string districts = " 1, 2, 3, 4, 5 ";
            int rating = 3;
            IActionResult result = _sc.SearchStore(keywoed, types, districts, rating);
            Assert.IsNotNull(result);
        }
        [Test]
        public void TestSearchNoKeyword()
        {
            IActionResult result = _sc.SearchStore(null, null, null, null);
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
        public void TestNoInputRestaurantID()
        {
            IActionResult result = _sc.Restaurant(null);
            Assert.IsNotNull(result);
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