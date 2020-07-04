using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Admin.Common;
using Admin.Models.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelViews;

namespace Admin.Controllers
{
    public class ImageProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForProduct(Guid productId)
        {
            ViewBag.Product = Models.Business.ImageBus.GetByProductId(productId).Result;
            return View();
        }
        [HttpGet]
        public IActionResult DeleteImage(Guid id, Guid productId)
        {
            if (ImageBus.DeleteImage(id).Result) TempData[ConstKey.Success] = "Deleted";
            else TempData[ConstKey.Error] = "Fail, TryAgain!";
            return RedirectToAction("ForProduct", "ImageProduct", new { productId });
        }


        [HttpPost]
        public IActionResult PostImage(ImageInputMv image, IFormFile fileData )
        {
            var imageProduct = new ImageMv();
            imageProduct.CreateBy = Guid.Parse("a845b16a-4ca6-48e2-4ca6-08d817450c1a");
            imageProduct.ProductId = image.ProductId;
            //convert to image to base 64
            var ms = new MemoryStream();
            fileData.CopyTo(ms);
            var fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);

            imageProduct.ImageLocation = s;

            if (ImageBus.PostImage(imageProduct).Result) TempData[ConstKey.Success] = "Add Success!";
            else TempData[ConstKey.Error] = "Fail, Try again !";
            return RedirectToAction("ForProduct", "ImageProduct", new { productId = image.ProductId });
        }
    }
}
