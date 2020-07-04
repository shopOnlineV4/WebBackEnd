using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Admin.Controllers
{
    public class TypeProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        //public IActionResult ColorProduct()
        //{
        //    ViewBag.Colors = TypeProductBus.GetAllColorCodesAsync().Result.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ColorProduct(ColorCodeMv color)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (TypeProductBus.CreateNewColorProductAsync(color).Result) TempData[ConstKey.Success] = "Success!";
        //        else TempData[ConstKey.Error] = "Fail! Try Again.";
        //        return RedirectToAction("ColorProduct");
        //    }
        //    ViewBag.Colors = TypeProductBus.GetAllColorCodesAsync().Result.ToList();
        //    return View();
        //}

        //public IActionResult DeleteColorProduct(int id)
        //{
        //    if (TypeProductBus.DeleteColorAsync(id).Result)
        //        TempData[ConstKey.Error] = "Delete Success!";
        //    else TempData[ConstKey.Error] = "Fail! Try Again.";
        //    return RedirectToAction("ColorProduct");
        //}

        //[HttpGet]
        //public IActionResult SizeProduct()
        //{
        //    ViewBag.Sizes = TypeProductBus.GetAllSizeCodesAsync().Result.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SizeProduct(SizeMv size)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (TypeProductBus.CreateNewSizeProductAsync(size).Result)
        //            TempData[ConstKey.Success] = "Success!";
        //        else TempData[ConstKey.Error] = "Fail! Try Again.";
        //        return RedirectToAction("SizeProduct");
        //    }
        //    ViewBag.Sizes = TypeProductBus.GetAllSizeCodesAsync().Result.ToList();
        //    return View();
        //}

        //public IActionResult DeleteSizeProduct(int id)
        //{
        //    if (TypeProductBus.DeleteSizeAsync(id).Result)
        //        TempData[ConstKey.Error] = "Delete Success!";
        //    else TempData[ConstKey.Error] = "Fail! Try Again.";
        //    return RedirectToAction("SizeProduct");
        //}


        //public IActionResult ForProduct(Guid productId)
        //{
        //    ViewBag.Product = TypeProductBus.GetByProductIdAsync(productId).Result;
        //    ViewBag.ColorCodes = TypeProductBus.GetAllColorCodesAsync().Result.ToList();
        //    ViewBag.Sizes = TypeProductBus.GetAllSizeCodesAsync().Result.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddTypeProduct(TypeProductMv typeProduct)
        //{

        //    var res = TypeProductBus.CreateNewTypeProductBusAsync(typeProduct).Result;
        //    if (res) TempData[ConstKey.Success] = "Success";
        //    else TempData[ConstKey.Error] = "Fail, Try again!";
        //    return RedirectToAction("ForProduct", "TypeProduct", new { productId = typeProduct.ProductId });
        //}

        //public IActionResult DeleteTypeProduct(Guid typeProductId, Guid productId )
        //{
        //    var res = TypeProductBus.DeleteTypeProductBusAsync(typeProductId).Result;
        //    if (res) TempData[ConstKey.Success] = "Success";
        //    else TempData[ConstKey.Error] = "Fail, Try again!";

        //    return RedirectToAction("ForProduct", "TypeProduct", new { productId = productId });
        //}


    }
}
