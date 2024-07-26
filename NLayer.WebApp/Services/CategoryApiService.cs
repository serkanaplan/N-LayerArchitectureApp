
using NLayer.Core.DTOs;

namespace NLayer.WebApp.Services;

public class CategoryApiService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("categories");
        return response.Data;
    }
}
