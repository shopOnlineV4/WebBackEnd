using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAdmin.Common;
using WebAdmin.Models.ApiService;
using WebAdmin.Models.Business;
using WebAdmin.Models.Enum;
using WebAdmin.Models.ModelView;

namespace WebAdmin.Controllers
{
    public class ProductController : Controller
    {
        private const string ModelName = "/api/Product";

        public IActionResult Index()
        {
            var products = ProductBus.GetAll().Result.ToList();
            var categories = CategoryBus.GetAll().Result.ToList();
            foreach (var product in products)
            {
                product.Category = categories.SingleOrDefault(x => x.Id == product.CategoryId);
            }
            ViewBag.Products = products;

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = CategoryBus.GetAll().Result.Where(x => x.CategoryParent == TypeCategories.Child).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductMv product)
        {
            if (!ModelState.IsValid) return View();
            if (product.FileImage != null)
            {
                var ms = new MemoryStream();
                product.FileImage.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                product.FileData = s;
                product.FileImage = null;
            }
            product.ModifiedBy = Guid.Parse("a845b16a-4ca6-48e2-4ca6-08d817450c1a");
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
            var product = ProductBus.GetById(id).Result;
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(ProductMv product)
        {
            ViewBag.Categories = CategoryBus.GetAll().Result.Where(x => x.CategoryParent == TypeCategories.Child).ToList();
            var productData = ProductBus.GetById(product.Id).Result;
            if (!ModelState.IsValid) return View(productData);
            product.ModifiedBy = Guid.Parse("a845b16a-4ca6-48e2-4ca6-08d817450c1a");
            if (product.FileImage != null)
            {
                var ms = new MemoryStream();
                product.FileImage.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                product.FileData = s;
                product.FileImage = null;
            }
            if (ProductBus.Update(product.Id, product).Result)
            {
                TempData[ConstKey.Success] = "Success!";
                return RedirectToAction("Index");
            }
            TempData[ConstKey.Error] = "Fail! Try again.";
            return View(productData);
        }

        public IActionResult Disable(Guid id)
        {
            if (ProductBus.DisableAsync(id).Result) TempData[ConstKey.Success] = "Success.";
            else TempData[ConstKey.Error] = "Fail! Try again.";
            return RedirectToAction("Index");
        }
    }
}
