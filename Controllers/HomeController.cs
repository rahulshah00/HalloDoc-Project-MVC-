﻿using HalloDoc_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HalloDoc_Project.Controllers
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
        public IActionResult patient_site()
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