using Microsoft.AspNetCore.Mvc;
using NhaSachOnline.Models;
using NhaSachOnline.Models.DTOs;
using NhaSachOnline.Repositories;
using System.Diagnostics;

namespace NhaSachOnline.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeRepository _homeRepository;

    public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
    {
      _homeRepository = homeRepository;
      _logger = logger;
    }

    public async Task<IActionResult> Index(string keySearch = "", int genreId = 0)
    {
      IEnumerable<Book> books = await _homeRepository.GetBooks();

      BookDisplayModel bookDisplayModel = new BookDisplayModel
      {
        Books = books,

        KeySearch = keySearch,
        GenreId = genreId

      };

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
