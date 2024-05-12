using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TripAdvisor.Models;
using System.Text.Json;
using TripAdvisor.Filters;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Org.BouncyCastle.Tls;
using TripAdvisor.Services;
using static System.Net.Mime.MediaTypeNames;

namespace TripAdvisor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISession _session;
        private DataManager dataManager;
        private TelegramBot bot;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor contextAccesor)
        {
            _logger = logger;
            _configuration = configuration;
            _session = contextAccesor.HttpContext.Session;
            dataManager = new DataManager(_configuration);
            contextAccesor.HttpContext.Items["utente"] = _session.GetString("utente");
            bot = new TelegramBot(_configuration.GetSection("Telegram")["apikey"], _configuration.GetSection("Telegram")["chatid"]);
        }

        public IActionResult Index()
        {
            if(_session.GetString("utente") == "admin")
            {
                ViewData["trips"] = dataManager.GetAllTrips();
            }
            else
            {
                ViewData["trips"] = dataManager.GetTrips();
            }

            return View();
        }
        public IActionResult TripDetails(int id)
        {
            ViewData["selectedTrip"] = dataManager.GetTrip(id);
            ViewData["comments"] = dataManager.GetComments(id);
            ViewData["manager"] = dataManager;
            return View();
        }
        [HttpPost]
        public IActionResult InsertComment(Comment comment)
        {
            dataManager.InsertComment(comment);
            bot.NewComment(comment);
            return View("Index");
        }
        [HttpPost]
        public IActionResult InsertTrip(Trip trip,IFormFile file)
        {
            dataManager.InsertTrip(trip,file);
            return View("Index");
        }
        [HttpPost]
        public IActionResult ApproveComment(int id)
        {
            dataManager.UpdateStateComment(id, "approved");
            return View("Index");
        }
        [HttpPost]
        public IActionResult RefuseComment(int id)
        {
            dataManager.UpdateStateComment(id, "refused");
            return View("Index");
        }
        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            dataManager.DeleteComment(id);
            return View("Index");
        }

        public IActionResult Register()
        {
            ViewData["classes"] = dataManager.GetClasses();
            return View(new RegistrationUser());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationUser user)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = _configuration.GetSection("Firebase")["apikey"],
                AuthDomain = _configuration.GetSection("Firebase")["authdomain"],
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);

            string email = user.email;
            string password = user.password;
            try
            {
                if(email.Length > 0 && password.Length > 0) //Controlli vari su email e password
                {
                    UserCredential userCredential = await client.CreateUserWithEmailAndPasswordAsync(email, password);
                    dataManager.InsertUser(new Models.User { uid = userCredential.User.Uid, classid = user.classid, name = user.name});
                    bot.NewUser(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewData["errore"] = "Credenziali errate!";
                    return View(new System.Net.NetworkCredential());
                }  
            }
            catch (Exception ex)
            {
                ViewData["errore"] = "Credenziali errate!";
                return View(new System.Net.NetworkCredential());
            }
            
        }
        public IActionResult Login()
        {
            return View(new System.Net.NetworkCredential());
        }

        [HttpPost]
        public async Task<IActionResult> Login(System.Net.NetworkCredential credential)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = _configuration.GetSection("Firebase")["apikey"],
                AuthDomain = _configuration.GetSection("Firebase")["authdomain"],
                Providers = new FirebaseAuthProvider[]
                {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                }
            };

            var client = new FirebaseAuthClient(config);

            string email = credential.UserName;
            string password = credential.Password;
            UserCredential userCredential = null;
            try
            {
                userCredential = await client.SignInWithEmailAndPasswordAsync(email, password);
                _session.SetString("UID", userCredential.User.Uid);
                if (userCredential.User.Uid.Equals(_configuration.GetSection("AdminUID")["mb"]) || userCredential.User.Uid.Equals(_configuration.GetSection("AdminUID")["ns"]))
                {
                    var s = client.User;
                    _session.SetString("utente", "admin");
                }
            }
            catch(Exception ex)
            {

            }
            if(userCredential != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(new System.Net.NetworkCredential());
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public IActionResult UploadImage(IFormFile file, int tripId)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        SaveImage(file, tripId);
        //        var trip = GetTripById(tripId);
        //        if (trip != null)
        //        {
        //            trip.Picture = $"{tripId}_{file.FileName}";
        //        }
        //        return Ok(new { Message = "Immagine caricata con successo" });
        //    }
        //    return BadRequest(new { Message = "Nessun file caricato" });
        //}

    }
}