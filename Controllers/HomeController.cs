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
            _context.Aspnetusers.Add(user);
            _context.SaveChanges();

            //user.Modifieddate = DateTime.Now;

            User user_obj =new User();
            user_obj.Firstname=pm.FirstName;
            user_obj.Lastname = pm.LastName;
            user_obj.Email=pm.Email;
            user_obj.Mobile = pm.PhoneNo;
            user_obj.Street = pm.Street;
            user_obj.City= pm.City; 
            user_obj.State= pm.State;
            user_obj.Zipcode = pm.ZipCode;
            user_obj.Createddate = DateTime.Now;
            user_obj.Createdby = id;
            //user_obj.Modifiedby = null;
            _context.Users.Add(user_obj);
            _context.SaveChanges();

            Request request = new Request();
            //change the fname, lname , and contact detials acc to the requestor
            request.Requesttypeid = 2;
            request.Userid =user_obj.Userid;
            request.Firstname = pm.FirstName;
            request.Lastname = pm.LastName;
            request.Phonenumber=pm.PhoneNo; 
            request.Email=pm.Email;
            request.Createddate = DateTime.Now;
            request.Patientaccountid = id;
            request.Status = 1;
            request.Createduserid = user_obj.Userid;
            _context.Requests.Add(request);
            _context.SaveChanges();

            Requestclient rc=new Requestclient();   
            rc.Requestid=request.Requestid;
            rc.Firstname= pm.FirstName;
            rc.Lastname= pm.LastName;
            rc.Phonenumber = pm.PhoneNo;
            rc.Location = pm.City + pm.State;
            rc.Email = pm.Email;
            rc.Address = pm.RoomSuite+", "+pm.Street + ", " + pm.City + ", " + pm.State + ", " + pm.ZipCode;
            rc.Street = pm.Street;
            rc.City = pm.City;
            rc.State = pm.State;
            rc.Zipcode = pm.ZipCode;
            rc.Notes = pm.Symptoms;

            _context.Requestclients.Add(rc);
            
            _context.SaveChanges();
            return RedirectToAction("create_patient_request","Home");
         }
        //public IActionResult PatientCheckEmail(string email)
        //{
        //    var existingUser = _context.Aspnetusers.FirstOrDefault(u => u.Email == email);
        //    bool isValidEmail;
        //    if (existingUser == null)
        //    {
        //        isValidEmail = false;
        //    }
        //    else
        //    {
        //        isValidEmail = true;
        //    }
        //    return Json(new { isValid = isValidEmail });
        //}
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            bool emailExists = _context.Users.Any(u => u.Email == email);
            return Json(new { exists = emailExists });

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
                return RedirectToAction("PatientDashboard");
            }
            return View();
        }

        public IActionResult patient_site()
        {
            return View();
        }

        public IActionResult ViewDocuments()
        {
            return View();
        }
        public IActionResult Business_Info()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Business_Info(BusinessModel bm)
        {
            Business bus = new()
            {
                Name = bm.BusinessName,
                Phonenumber=bm.BsPhoneNo,
                Createddate = DateTime.Now
            };
            _context.Businesses.Add(bus);
            _context.SaveChanges();

            Request req = new()
            {
                Requesttypeid = 1,
                Firstname = bm.BsFirstName,
                Lastname = bm.BsLastName,
                Phonenumber = bm.BsPhoneNo,
                Email = bm.BsEmail,
                Status = 1,
                Createddate= DateTime.Now,
                
                //*****************confirmation-pending**************
                //Patientaccountid
                //Createduserid
            };
            _context.Requests.Add(req);
            _context.SaveChanges();
            Requestbusiness ReqBus = new()
            {
                Requestid = req.Requestid,
                Businessid = bus.Id,

            };

            _context.Requestbusinesses.Add(ReqBus);
            _context.SaveChanges();

            Requestclient rc = new()
            {
                Requestid = req.Requestid,
                Firstname = bm.PtFirstName,
                Lastname = bm.BsLastName,
                Phonenumber= bm.BsPhoneNo,
                Street=bm.Street,
                City=bm.city,
                State=bm.state,
                Zipcode=bm.zipcode
            };
            _context.Requestclients.Add(rc);
            _context.SaveChanges();
            return RedirectToAction("PatientDashboard");
        }

        public IActionResult Concierge_info()
        {
            return View();
        }
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Concierge_info(ConciergeModel cm)
        {
            Concierge c = new()
            {
                Conciergename = cm.ConFirstName + cm.ConLastName,
                Street=cm.ConStreet,
                City= cm.ConCity,
                State=cm.ConState,
                Zipcode=cm.ConZipCode,
                Createddate=DateTime.Now,
                
            };
            _context.Concierges.Add(c);
            _context.SaveChanges();
            Request req = new()
            {
                Requesttypeid=4,
                Firstname=cm.ConFirstName, 
                Lastname=cm.ConLastName,
                Phonenumber=cm.ConPhoneNo,
                Email=cm.ConEmail,
                Status=1,
                Createddate = DateTime.Now,

            };
            _context.Requests.Add(req);
            _context.SaveChanges();
            Requestconcierge rc = new()
            {
                Requestid = req.Requestid,
                Conciergeid = c.Conciergeid
            };
            _context.Requestconcierges.Add(rc);
            _context.SaveChanges();
            Requestclient rcl = new()
            {
                Requestid = req.Requestid,
                Firstname = cm.PtFirstName,
                Lastname = cm.PtLastName, Phonenumber=cm.PtPhoneNo, Email=cm.PtEmail,
                Street = cm.ConStreet,
                City = cm.ConCity,
                State = cm.ConState,
                Zipcode = cm.ConZipCode
            };
            _context.Requestclients.Add(rcl);
            _context.SaveChanges();
            return RedirectToAction("PatientDashboard");

        }

        public IActionResult PatientDashboard()
        {
            return View();
        }

        public IActionResult Friend_family()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Friend_family(FamilyFriends fmfr)
        {
            Request r=new()
            {
                Requesttypeid=3,
                Firstname=fmfr.firstName,
                Lastname=fmfr.lastName,
                Phonenumber=fmfr.phone,
                Email=fmfr.email,
                Status=1,
                Createddate=DateTime.Now
            };
            _context.Requests.Add(r);
            _context.SaveChanges();
            Requestclient rcl = new()
            {
                Requestid = r.Requestid,
                Firstname = fmfr.PatientModel.FirstName,
                Lastname = fmfr.PatientModel.LastName,
                Phonenumber = fmfr.PatientModel.PhoneNo,
                Email = fmfr.PatientModel.Email,
                Location = fmfr.PatientModel.City + fmfr.PatientModel.State,
                City=fmfr.PatientModel.City,
                State=fmfr.PatientModel.State,
                Zipcode=fmfr.PatientModel.ZipCode

            };
            _context.Requestclients.Add(rcl);
            _context.SaveChanges();

            return RedirectToAction("PatientDashboard");
        }

        public IActionResult submit_request_page()
        {
            return View();
        }
        public IActionResult ReviewAgreement()
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