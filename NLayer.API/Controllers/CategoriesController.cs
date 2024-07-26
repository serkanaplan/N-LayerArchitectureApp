using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class CategoriesController(ICategoryService categoryService, IMapper mapper) : CustomBaseController
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

        return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(200,categoriesDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        var categoryDto = _mapper.Map<CategoryDto>(category);

        return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200,categoryDto));
    }

    [HttpPost]
    public async Task<IActionResult> Save(CategoryDto categoryDto)
    {
        var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

        var categoryDto2 = _mapper.Map<CategoryDto>(category);

        return CreateActionResult(CustomResponseDto<CategoryDto>.Success(201,categoryDto2));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryDto categoryDto)
    {
        await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));

        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        
        await _categoryService.RemoveAsync(category);

        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }


    [HttpGet("[action]/{categoryId}")]
    public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
    {
        return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
    }
}
