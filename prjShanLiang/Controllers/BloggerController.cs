using Microsoft.AspNetCore.Mvc;
using prjShanLiang.Models;
using prjShanLiang.ViewModels;

namespace prjShanLiang.Controllers
{
    public class BloggerController : Controller
    {
        public IActionResult BloggerList(CKeywordViewModel vm)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            IEnumerable<Blog> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
                datas = from c in db.Blogs
                        select c;
            else
                datas = db.Blogs.Where(b => b.BlogHeader.Contains(vm.txtKeyword));
            return View(datas);
        }
        public IActionResult BloggerCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BloggerCreate(Blog b)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            db.Blogs.Add(b);
            db.SaveChanges();
            return RedirectToAction("List");
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
            return View();
        }
        public IActionResult BloggerEdit(int? id)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog blog = db.Blogs.FirstOrDefault(t => t.BlogId == id);
            if (blog == null)
                return RedirectToAction("List");
            return View(blog);
        }
        [HttpPost]
        public IActionResult BloggerEdit(Blog b)
        {
            ShanLiang21Context db = new ShanLiang21Context();
            Blog blog = db.Blogs.FirstOrDefault(t => t.BlogId == b.BlogId);
            if (blog != null)
            {
                blog.BlogHeader = b.BlogHeader;
                blog.BlogContent = b.BlogContent;
                blog.BlogPic = b.BlogPic;
     
            }
                return RedirectToAction("List");
            
        }
    }
}
