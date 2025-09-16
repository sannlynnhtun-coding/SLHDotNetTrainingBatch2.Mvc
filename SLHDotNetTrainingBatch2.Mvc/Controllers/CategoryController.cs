﻿using Microsoft.AspNetCore.Components;
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

        [ActionName("Create")]
        public IActionResult CategoryCreate()
        {
            return View("CategoryCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CategorySaveAsync(CategoryViewModel requestModel)
        {
            await _db.TblCategories.AddAsync(new TblCategory
            {
                CategoryId = Ulid.NewUlid().ToString(),
                CategoryCode = requestModel.CategoryCode,
                CategoryName = requestModel.CategoryName,
            });
            var result = await _db.SaveChangesAsync();
            var model = new CategoryViewResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful" : "Saving Failed"
            };
            return Json(model);
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> CategoryEditAsync(string id)
        {
            var category = await _db.TblCategories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName
            };
            return View("CategoryEdit", model);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> CategoryUpdateAsync(CategoryViewModel requestModel)
        {
            var category = await _db.TblCategories.FirstOrDefaultAsync(x => x.CategoryId == requestModel.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            category.CategoryCode = requestModel.CategoryCode;
            category.CategoryName = requestModel.CategoryName;
            var result = await _db.SaveChangesAsync();
            var model = new CategoryViewResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : "Updating Failed"
            };
            return Json(model);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<IActionResult> CategoryDeleteAsync(string id)
        {
            var category = await _db.TblCategories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _db.TblCategories.Remove(category);
            var result = await _db.SaveChangesAsync();
            var model = new CategoryViewResponseModel
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful" : "Deleting Failed"
            };
            return Json(model);
        }

    }

    public class CategoryViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
    
    public class CategoryViewResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
