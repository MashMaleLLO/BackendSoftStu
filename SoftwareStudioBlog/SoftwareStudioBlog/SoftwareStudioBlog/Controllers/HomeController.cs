using Microsoft.AspNetCore.Mvc;
using SoftwareStudioBlog.Models;
using System.Diagnostics;

namespace SoftwareStudioBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}