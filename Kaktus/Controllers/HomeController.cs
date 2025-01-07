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
namespace Kaktus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IFileManagerService fileManager;
        private IFileRepository fileRepository;
        public HomeController(IUserRepository userRepository, IFileManagerService fileManager, IFileRepository fileRepository)
        {
            this.userRepository = userRepository;
            this.fileManager = fileManager;
            this.fileRepository = fileRepository;
        }

        [Authorize]
        public IActionResult Index(FileViewModel model)
        {
            ViewBag.Files = fileManager.GetAllUsersFiles();
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
                fileManager.AddFile(model);
                return RedirectToAction("Index");
            }
            else
            {
                // Notify.ShowError("Incorrect data, refill the form", 5);
                return View("Index", model);
            }

        }
        public IActionResult DowloadFile(string id)
        {
            DownloadedFile file = fileManager.GetFileBytesById(id);
            if (file.IsPassword) { ViewBag.IdFile = id; return View(); }
            if (file != null) { return File(file.BytesFile, "application/octet-stream", file.FileName); }
            else { return RedirectToAction("FileNotFound"); }
        }

        [HttpPost]
        public IActionResult DowloadFile(string id, string password)
        {
            DownloadedFile file = fileManager.GetFileBytesById(id, password);
            if (file.State == StateExc.FileNotFound)
            {
                return RedirectToAction("FileNotFound");
            }
            else if (file.State == StateExc.WrongPassword)
            {
                ViewBag.IdFile = id;
                ViewBag.PassFile = password;
                return View();
            }
            else
            {
                //Response.Redirect("Index");
                return File(file.BytesFile, "application/octet-stream", file.FileName);
            }
        }
        public IActionResult FileNotFound()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteFile(string id)
        {
            fileManager.DeleteFile(id);
            FileModel file = fileRepository.Find(id);
            fileRepository.Remove(file);
            fileRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
