using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Common;
using Admin.Models.ApiService;
using Admin.Models.Business;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using ModelViews.Enum;
using Newtonsoft.Json;


namespace Admin.Controllers
{
    public class CategoryController : Controller
    {
        private const string ModelName = "/api/category";


        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await CategoryBus.GetAll();
            return View();
        }
        public IActionResult Delete(Guid id)
        {
            var res = ServiceApi.DeleteDataById(ModelName, id).Result;
            TempData[ConstKey.Error] = "Deleted";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryInput category)
        {
            if (!ModelState.IsValid) return View();
            category.CategoryParent = TypeCategories.Parent;
            var res = ServiceApi.PostData<CategoryInput>(ModelName, category).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData[ConstKey.Success] = "Success";
                return RedirectToAction("Index");
            }
            TempData[ConstKey.Error] = "Error,Try Again! ";
            return View();
        }
        [HttpPost]
        public IActionResult CreateChild(CategoryInput category)
        {
            if (!ModelState.IsValid) return RedirectToAction("DetailParent", "Category", new { id = category.SubCategoryId }); ;
            bool res = CategoryBus.CreateChild(category);
            if (res)
                TempData[ConstKey.Success] = "Success";
            else TempData[ConstKey.Error] = "Error,Try Again! ";
            return RedirectToAction("DetailParent", "Category", new { id = category.SubCategoryId });
        }

        [HttpPost]
        public IActionResult Update(Guid id, CategoryInput category)
        {
            if (!ModelState.IsValid) return RedirectToAction("DetailParent", "Category", new { id = id });
            var res = CategoryBus.UpdateCategory(id, category);
            if (res) TempData[ConstKey.Success] = "Success";
            else TempData[ConstKey.Error] = "Error,Try Again! ";
            return RedirectToAction("DetailParent", "Category", new { id = id });
        }

        [HttpGet]
        public IActionResult DetailParent(Guid id)
        {
            ViewBag.CategoryParrent = CategoryBus.GetById(id).Result;
            if (ViewBag.CategoryParrent == null) return RedirectToAction("Index");
            ViewBag.CategoryChild = CategoryBus.GetAll().Result.Where(x => x.SubCategoryId == id).ToList();
            return View();
        }
    }
}