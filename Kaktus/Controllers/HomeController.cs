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
        public IActionResult AddFile(IFormFile file, string name, string tag, string emailTo)
        {
            FileViewModel fileViewModel = new FileViewModel()
            {
                File = file,
                Name = name,
                Tag = tag,
                EmailTo = emailTo
            };
            if (fileViewModel.IsValid())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("MODEL INVALID! \n The next update developed");
            }
        }

    }
}
