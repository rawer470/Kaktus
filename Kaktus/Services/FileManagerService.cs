using System;
using Kaktus.Models;
using Kaktus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Kaktus.Services;

public class FileManagerService : IFileManagerService
{
    public bool AddFile(FileViewModel fileView)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadedDirectory = Path.Combine(currentDirectory, $"UploadFiles\\{User.Identity.Name}");
        if (!Directory.Exists(uploadedDirectory)) { Directory.CreateDirectory(uploadedDirectory); }
        var filepath = Path.Combine(uploadedDirectory, $"{fileView.Name}.{fileView.File.ContentType.Split('/')[1]}");
        using (var stream = new FileStream(filepath, FileMode.Create))
        {
            fileView.File.CopyTo(stream);
        }
        
    }

    public bool DeleteFile(FileModel file)
    {
        throw new NotImplementedException();
    }

    public FileModel GetFileById(int id)
    {
        throw new NotImplementedException();
    }
}
