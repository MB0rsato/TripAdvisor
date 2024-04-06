using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TripAdvisor.Models;

namespace TripAdvisor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ProvaTelegram()
        {
            HttpClient cliente = new HttpClient();
            string apikey = "7137023147:AAGza7NQA0T9PkO3kEUnFoyI9JjJNzv-vys";
            string chatid = "-4173595237";
            Uri uri = new Uri("https://api.telegram.org/bot7137023147:AAGza7NQA0T9PkO3kEUnFoyI9JjJNzv-vys/sendMessage?chat_id=-4173595237&text=DIOCAN%20CHE%20FIGA");
            var response = await cliente.GetAsync(uri);
            ViewData["esito"] = "Successo = " + response;
            return View("index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}