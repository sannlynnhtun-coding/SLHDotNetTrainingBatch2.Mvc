using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SLHDotNetTrainingBatch2.Database.AppDbContextModels;

namespace SLHDotNetTrainingBatch2.Mvc.Controllers
{
    // https://localhost:3000/Blog/BlogIndex
    // https://localhost:3000/Blog/Index
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        [ActionName("Index")]
        public async Task<IActionResult> BlogIndex()
        {
            var lst = await _db.TblBlogs.ToListAsync();
            return View("BlogIndex", lst);

            //var model = new BlogResponseModel
            //{
            //    Blogs = await _db.TblBlogs.ToListAsync(),
            //    BlogV2s = await _db.TblBlogs.ToListAsync()
            //};
            //return View("BlogIndex", model);
        }

        [ActionName("Generate")]
        public async Task<IActionResult> BlogGenerate()
        {
            for (int i = 0; i <= 19; i++)
            {
                int rowNo = i + 1;
                var blog = new TblBlog
                {
                    BlogId = Ulid.NewUlid().ToString(),
                    BlogTitle = "My First Blog " + rowNo,
                    BlogAuthor = "Admin " + rowNo,
                    BlogContent = "This is my first blog content. " + rowNo,
                    CreatedBy = "Admin",
                    CreatedDateTime = DateTime.Now,
                    DeleteFlag = false
                };
                _db.TblBlogs.Add(blog);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }

    //public class BlogResponseModel
    //{
    //    public List<TblBlog> Blogs { get; set; }
    //    public List<TblBlog> BlogV2s { get; set; }
    //}
}
