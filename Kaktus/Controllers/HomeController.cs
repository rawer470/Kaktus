using Kaktus.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text.Json;
using Kaktus.NotifyClasses;
using Kaktus.Data;
using Microsoft.AspNetCore.Authorization;
using Kaktus.Services.Interfaces;
using NotificationExtensions;

namespace Kaktus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public HomeController(IUserRepository userRepository)
        {
        this.
        }

        [Authorize]
        public IActionResult Index(FileViewModel model)
        {
            return View(new FileViewModel());
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(FileViewModel model) //IFormFile file, 
        {
            if (ModelState.IsValid)
            {
                
                var currentDirectory = Directory.GetCurrentDirectory();
                var uploadedDirectory = Path.Combine(currentDirectory, $"UploadFiles\\{User.Identity.Name}");
                if (!Directory.Exists(uploadedDirectory)) { Directory.CreateDirectory(uploadedDirectory); }
                var filepath = Path.Combine(uploadedDirectory, $"{model.Name}.{model.File.ContentType.Split('/')[1]}");
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
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
