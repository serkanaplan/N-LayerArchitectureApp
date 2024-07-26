using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.WebApp.Services;

namespace NLayer.WebApp.Controllers;

public class ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService) : Controller
{

    private readonly ProductApiService _productApiService = productApiService;
    private readonly CategoryApiService _categoryApiService = categoryApiService;

    public async Task<IActionResult> Index() => View(await _productApiService.GetProductsWithCategoryAsync());

    public async Task<IActionResult> Save()
    {
        var categoriesDto = await _categoryApiService.GetAllAsync();
        ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Save(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            await _productApiService.SaveAsync(productDto);
            return RedirectToAction(nameof(Index));
        }
        var categoriesDto = await _categoryApiService.GetAllAsync();
        ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
        return View();
    }


    public async Task<IActionResult> Update(int id)
    {
        var product = await _productApiService.GetByIdAsync(id);
        var categoriesDto = await _categoryApiService.GetAllAsync();
        ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
        return View(product);
    }


    [HttpPost]
    public async Task<IActionResult> Update(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            await _productApiService.UpdateAsync(productDto);
            return RedirectToAction(nameof(Index));
        }
        var categoriesDto = await _categoryApiService.GetAllAsync();
        ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
        return View(productDto);
    }


    public async Task<IActionResult> Remove(int id)
    {
        await _productApiService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
