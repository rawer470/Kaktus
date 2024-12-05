using Kaktus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using Kaktus.NotifyClasses;
using Kaktus.Data;

namespace Kaktus.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(INotyfService notifyService, Context context)
        {
            Notify.Configure(notifyService);
            context.Users.Add(new Models.User{
                UserName ="Test",
                
            });
            context.SaveChanges();
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
