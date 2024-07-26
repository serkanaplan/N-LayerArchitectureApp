using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.WebApp.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(ErrorViewModel errorViewModel) => View(errorViewModel);
}
