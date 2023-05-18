using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;

namespace prjShanLiang.Controllers
{
    public class BloggerController : Controller
    {
        private IWebHostEnvironment _enviro;
        public BloggerController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IActionResult BloggerList(CKeywordViewModel vm)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            IEnumerable<Blog> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Blogs
                        select c;
            else
                datas = db.Blogs.Where(b => b.BlogHeader.Contains(vm.txtKeyword) ||
                b.CityName.Contains(vm.txtKeyword) || 
                b.DistrictName.Contains(vm.txtKeyword) ||
                b.RestaurantName.Contains(vm.txtKeyword));
            return View(datas);
        }
        public IActionResult BloggerCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BloggerCreate(Blog blog)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            db.Blogs.Add(blog);
           
                db.SaveChanges();
            return RedirectToAction("BloggerList");
        }
        public IActionResult BloggerDelete(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog b = db.Blogs.FirstOrDefault(t => t.BlogId == id);
            if(b != null)
            {
                db.Blogs.Remove(b);
                db.SaveChanges();
            }
            return RedirectToAction("BloggerList");
        }
        public IActionResult BloggerEdit(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog blog = db.Blogs.FirstOrDefault(t => t.BlogId == id);
            if (blog == null)
                return RedirectToAction("BloggerList");
            return View(blog);
        }
        [HttpPost]
        public IActionResult BloggerEdit(CBlogwrap p)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog blog = db.Blogs.FirstOrDefault(t => t.BlogId == p.BlogId);
            if (blog != null)
            {
                if (p.photo != null)
                {
                    string PicName = Guid.NewGuid().ToString() + ".jpg";
                    string path = _enviro.WebRootPath + "/Images/Blog/" + PicName;
                    p.photo.CopyTo(new FileStream(path, FileMode.Create));
                    blog.BlogPic = PicName;
                }
                blog.BlogHeader = p.BlogHeader;
                blog.BlogContent = p.BlogContent;
                blog.CityName = p.CityName;
                blog.DistrictName = p.DistrictName;
                blog.RestaurantName = p.RestaurantName;
               
                db.SaveChanges();
            }
                return RedirectToAction("BloggerList");         
        }
        public IActionResult BloggerDetail(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog blog = db.Blogs.FirstOrDefault(t => t.BlogId == id);
            if (blog == null)
                return RedirectToAction("BloggerList");
            return View(blog);
        }
    }
}
