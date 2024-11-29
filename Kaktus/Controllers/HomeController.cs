using Kaktus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using Kaktus.NotifyClasses;

namespace Kaktus.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(INotyfService notifyService)
        {
            Notify.Configure(notifyService);
        }

        public IActionResult Index(FileViewModel model)
        {
            return View(new FileViewModel());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(FileViewModel model) //IFormFile file, 
        {
            if (ModelState.IsValid)
            {
                Notify.ShowSuccess("File uploaded", 5);
                return RedirectToAction("Index");
            }
            else
            {
                Notify.ShowError("Incorrect data, refill the form", 5);
                return View("Index", model);
            }

        }

    }
}
