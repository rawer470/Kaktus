using Kaktus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text.Json;

namespace Kaktus.Controllers
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

        [HttpPost]
        public IActionResult AddFileAA(FileViewModel file)
        {
            if (ModelState.IsValid)
            {
                return Content("OK");
            }
            else
            {
                return RedirectToAction("Index", file);
            }
        }

    }
}
