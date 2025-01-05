using System;
using BCrypt.Net;
using Kaktus.Models;
using Kaktus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Kaktus.Services;

public class FileManagerService : IFileManagerService
{
    private IHttpContextAccessor httpContext;
    private IFileRepository fileRepository;
    private UserManager<User> userManager;
    public FileManagerService(IHttpContextAccessor httpContext, IFileRepository fileRepository, UserManager<User> userManager)
    {
        this.httpContext = httpContext;
        this.fileRepository = fileRepository;
        this.userManager = userManager;
    }

    public bool AddFile(FileViewModel fileView)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadedDirectory = Path.Combine(currentDirectory, $"UploadFiles\\{httpContext.HttpContext?.User.Identity.Name}");
        if (!Directory.Exists(uploadedDirectory)) { Directory.CreateDirectory(uploadedDirectory); }
        var filepath = Path.Combine(uploadedDirectory, $"{fileView.Name}.{fileView.File.ContentType.Split('/')[1]}");
        using (var stream = new FileStream(filepath, FileMode.Create))
        {
            fileView.File.CopyTo(stream);
        }
        if (fileView.Password != null)
        {
            FileModel file = new FileModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = fileView.Name,
                Path = filepath,
                Tag = fileView.Tag,
                IdUser = userManager.GetUserId(httpContext.HttpContext?.User),
                FileType = fileView.File.ContentType.Split('/')[1],
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(fileView.Password)
            };
            AddFileToDbAsync(file);
        }
        else
        {
            FileModel file = new FileModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = fileView.Name,
                Path = filepath,
                Tag = fileView.Tag,
                IdUser = userManager.GetUserId(httpContext.HttpContext?.User),
                FileType = fileView.File.ContentType.Split('/')[1],
            };
            AddFileToDbAsync(file);
        }
        return false;
    }

    public async Task AddFileToDbAsync(FileModel fileModel)
    {
        fileRepository.Add(fileModel);
    }
    public List<FileModel> GetAllUsersFiles()
    {
        string UserId = userManager.GetUserId(httpContext.HttpContext.User);
        List<FileModel> files = fileRepository.GetFilesFromUser(UserId);
        return files;
    }
    public bool DeleteFile(string id)
    {
        var file = GetFileById(id);
        if (!System.IO.File.Exists(file.Path))
        {
            return false;
        }
        System.IO.File.Delete(file.Path);
        return true;
    }
    public FileModel GetFileById(string id)
    {
        return fileRepository.FirstOrDefault(x => x.Id == id);
    }
    public FileModel GetFileById(string id, string password)
    {
        var file = fileRepository.FirstOrDefault(x => x.Id == id);
        if (file == null)
        {
            file.state = StateExc.FileNotFound;
            return file;
        }
        else if (password == null)
        {
            file.state = StateExc.WrongPassword;
            return file;
        }
        if (BCrypt.Net.BCrypt.Verify(password, file.PasswordHash))
        {
            file.state = StateExc.OK;
            return file;
        }
        else
        {
            file.state = StateExc.WrongPassword;
            return file;
        }
        return null;
    }
    public DownloadedFile GetFileBytesById(string id)
    {
        DownloadedFile file = new DownloadedFile();
        FileModel fileModel = GetFileById(id);
        if (fileModel.PasswordHash != null)
        {
            file.IsPassword = true;
            return file;
        }
        if (fileModel == null) { return null; }
        file.FileName = $"{fileModel.Name}.{fileModel.FileType}";
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), $"UploadFiles\\{httpContext.HttpContext?.User.Identity.Name}");
        string filePath = Path.Combine(uploadFolder, file.FileName);
        if (!System.IO.File.Exists(filePath)) { return null; }
        file.BytesFile = System.IO.File.ReadAllBytes(filePath);
        return file;
    }
    public DownloadedFile GetFileBytesById(string id, string password)
    {
        DownloadedFile file = new DownloadedFile();
        FileModel fileModel = GetFileById(id, password);
        if (fileModel.state == StateExc.FileNotFound)
        {
            file.State = StateExc.FileNotFound;
            return file;
        }
        else if (fileModel.state == StateExc.WrongPassword)
        {
            file.State = StateExc.WrongPassword;
            return file;
        }
        file.FileName = $"{fileModel.Name}.{fileModel.FileType}";
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), $"UploadFiles\\{httpContext.HttpContext?.User.Identity.Name}");
        string filePath = Path.Combine(uploadFolder, file.FileName);
        if (!System.IO.File.Exists(filePath))
        {
            file.State = StateExc.FileNotFound;
            return file;
        }
        file.BytesFile = System.IO.File.ReadAllBytes(filePath);
        return file;
    }
}
