using DAL.DataModels;
using HalloDoc_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DAL.DataContext;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create_patient_request(PatientModel pm)
        {
            //var newvm=new PatientModel();
            Aspnetuser user = new Aspnetuser();
            
            string id=Guid.NewGuid().ToString();
            user.Id = id;
            user.Email = pm.Email;
            user.Phonenumber = pm.PhoneNo;
            user.Username = pm.FirstName;
            user.Createddate = DateTime.Now;
            
            //user.Modifieddate = DateTime.Now;
            
            User user_obj=new User();
            user_obj.Firstname=pm.FirstName;
            user_obj.Lastname = pm.LastName;
            user_obj.Email=pm.Email;
            user_obj.Mobile = pm.PhoneNo;
            user_obj.Street = pm.Street;
            user_obj.City= pm.City; 
            user_obj.State= pm.State;
            user_obj.Zipcode = pm.ZipCode;
            user_obj.Createddate = DateTime.Now;
            //user_obj.Modifiedby = null;

            
            Request request = new Request();
            request.Firstname = pm.FirstName;
            request.Lastname = pm.LastName;
            request.Phonenumber=pm.PhoneNo; 
            request.Email=pm.Email;
            request.Createddate = DateTime.Now;
            request.Patientaccountid = id;

            Requestclient rc = new Requestclient();
            


            _context.Aspnetusers.Add(user);
            _context.SaveChanges();
            return RedirectToAction("create_patient_request","Home");
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

            Aspnetuser v = _context.Aspnetusers.FirstOrDefault(dt => dt.Username == demouser.Username && dt.Passwordhash == demouser.Passwordhash);
            if (v != null)
            {
                return RedirectToAction("Business_Info");
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