using Microsoft.AspNetCore.Mvc;
using NGINX.Models;
using System.Diagnostics;

namespace NGINX.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesServices _gamesServices;

        public HomeController(IGamesServices gamesServices)
        {
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
            var Games = _gamesServices.GetAll();
            return View(Games);
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
