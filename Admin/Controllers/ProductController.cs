using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Admin.Common;
using Admin.Models.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelViews;
using ModelViews.Enum;

namespace WebAdmin.Controllers
{
    public class ProductController : Controller
    {
        private const string ModelName = "/api/Product";

        public IActionResult Index()
        {
            var products = ProductBus.GetAll().Result.ToList();

            ViewBag.Products = products;

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryBus.GetAll().Result.Where(x => x.CategoryParent == TypeCategories.Child).ToList();
            return View();
        }
        //insert new 
        [HttpPost]
        public IActionResult Create(IFormFile filepImageProduct, InsertProduct product)
        {
            if (!ModelState.IsValid) return View();
            if (filepImageProduct != null)
            {
                var ms = new MemoryStream();
                filepImageProduct.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                product.FileData = s;

            }
            product.CreateBy = Guid.Parse("6eac70b2-50eb-42fb-00a7-08d81e5fa53a");
            if (ProductBus.Post(product).Result)
            {
                TempData[ConstKey.Success] = "Success!";
                return RedirectToAction("Index");
            }
            TempData[ConstKey.Error] = "Fail! Try again.";
            return View();
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            ViewBag.Categories = CategoryBus.GetAll().Result.Where(x => x.CategoryParent == TypeCategories.Child).ToList();
            ViewBag.product = ProductBus.GetById(id).Result;
            return View();
        }
        [HttpPost]
        public IActionResult Update(Guid id, IFormFile fileInput, InsertProduct product)
        {
            ViewBag.Categories = CategoryBus.GetAll().Result.Where(x => x.CategoryParent == TypeCategories.Child).ToList();

            ViewBag.product = ProductBus.GetById(id).Result;
            if (!ModelState.IsValid) return View();
            product.ModifiedBy = Guid.Parse("a845b16a-4ca6-48e2-4ca6-08d817450c1a");
            if (fileInput != null)
            {
                var ms = new MemoryStream();
                fileInput.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                product.FileData = s;

            }
            if (ProductBus.Update(id, product).Result)
            {
                TempData[ConstKey.Success] = "Success!";
                return RedirectToAction("Index");
            }
            TempData[ConstKey.Error] = "Fail! Try again.";
            return View();
        }

        public IActionResult Disable(Guid id)
        {
            if (ProductBus.DisableAsync(id).Result) TempData[ConstKey.Success] = "Success.";
            else TempData[ConstKey.Error] = "Fail! Try again.";
            return RedirectToAction("Index");
        }
    }
}
