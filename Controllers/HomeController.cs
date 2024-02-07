using HalloDoc_Project.DataContext;
using HalloDoc_Project.DataModels;
using HalloDoc_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HalloDoc_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
         public IActionResult patient_submit_request_screen()
        {
            return View();
        }
        public IActionResult create_patient_request()
        {
            return View();
        }
        public IActionResult forgot_password_page()
        {
            return View();
        }
        public IActionResult login_page()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult login_page(Aspnetuser demouser)
        {
            var  obj = _context.Aspnetusers.ToList();

            foreach (var item in obj)
            {
                if(demouser.Username == item.Username && demouser.Passwordhash == item.Passwordhash)
                {
                   return View("create_patient_request"); 
                   
                }
            }
            return View();
        }
        public IActionResult patient_site()
        {
            return View();
        }

        public IActionResult Business_Info()
        {
            return View();
        }
        public IActionResult Concierge_info()
        {
            return View();
        }

        public IActionResult Friend_family()
        {
            return View();
        }

        public IActionResult submit_request_page()
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