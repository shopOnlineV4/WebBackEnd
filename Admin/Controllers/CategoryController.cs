using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Common;
using Admin.Models.ApiService;
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
            var res = await ServiceApi.GetData(ModelName);
            if (res.IsSuccessStatusCode)
                ViewBag.Categories = JsonConvert.DeserializeObject<List<CategoryMv>>(res.Content.ReadAsStringAsync().Result.ToString());
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
        public IActionResult Create(CategoryMv category)
        {
            if (!ModelState.IsValid) return View();
            category.CategoryParent = TypeCategories.Parent;
            var res = ServiceApi.PostData<CategoryMv>(ModelName, category).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData[ConstKey.Success] = "Success";
                return RedirectToAction("Index");
            }
            TempData[ConstKey.Error] = "Error,Try Again! ";
            return View();
        }
        [HttpPost]
        public IActionResult CreateChild(CategoryMv category)
        {
            if (!ModelState.IsValid) return RedirectToAction("DetailParent", "Category", new { id = category.SubCategoryId }); ;
            category.CategoryParent = TypeCategories.Child;
            var res = ServiceApi.PostData<CategoryMv>(ModelName, category).Result;
            if (res.IsSuccessStatusCode)
                TempData[ConstKey.Success] = "Success";
            else TempData[ConstKey.Error] = "Error,Try Again! ";
            return RedirectToAction("DetailParent", "Category", new { id = category.SubCategoryId });
        }

        [HttpPost]
        public IActionResult Update(CategoryMv category)
        {
            if (!ModelState.IsValid) return RedirectToAction("DetailParent", "Category", new { id = category.Id }); ;
            category.CategoryParent = TypeCategories.Child;
            var res = ServiceApi.Update<CategoryMv>(ModelName, category.Id, category).Result;
            if (res.IsSuccessStatusCode) TempData[ConstKey.Success] = "Success";
            else TempData[ConstKey.Error] = "Error,Try Again! ";
            return RedirectToAction("DetailParent", "Category", new { id = category.Id });
        }



        [HttpGet]
        public IActionResult DetailParent(Guid id)
        {
            var res = ServiceApi.GetDataById(ModelName, id).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<CategoryMv>(res.Content.ReadAsStringAsync().Result.ToString());
                return View(data);
            }
            return RedirectToAction("Index");
        }
    }
}