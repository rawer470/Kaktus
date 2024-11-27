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
            return View(new FileViewModel(){
                Name="Write in file name",
                Tag = "#000000",
                EmailTo = "example@example.com"
            });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFile(IFormFile file, FileViewModel model)
        {
            if (ModelState.IsValid && file != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
               //return RedirectToAction("Index", model);
            }
            return Content("");
        }

    }
}
