using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SLHDotNetTrainingBatch2.Database.AppDbContextModels;

namespace SLHDotNetTrainingBatch2.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        [ActionName("Index")]
        public IActionResult CategoryIndex()
        {
            return View("CategoryIndex");
        }

        [ActionName("List")]
        public async Task<IActionResult> CategoryListAsync()
        {
            var result = await _db.TblCategories.ToListAsync();
            List<CategoryViewModel> lst = result.Select(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryCode = x.CategoryCode,
                CategoryName = x.CategoryName
            }).ToList();

            //List<CategoryViewModel> lst = new List<CategoryViewModel>();
            //foreach (var x in result)
            //{
            //    var item = new CategoryViewModel
            //    {
            //        CategoryId = x.CategoryId,
            //        CategoryCode = x.CategoryCode,
            //        CategoryName = x.CategoryName
            //    };
            //    lst.Add(item);
            //}
            return Json(lst);
        }
    }

    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
