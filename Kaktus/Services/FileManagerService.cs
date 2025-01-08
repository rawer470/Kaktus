using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Kaktus.Models;
using Kaktus.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Kaktus.Services;

public class FileManagerService : IFileManagerService
{
    private Thread threadCrypto;
    private IHttpContextAccessor httpContext;
    private IFileRepository fileRepository;
    private UserManager<User> userManager;
    public FileManagerService(IHttpContextAccessor httpContext, IFileRepository fileRepository, UserManager<User> userManager)
    {
        this.httpContext = httpContext;
        this.fileRepository = fileRepository;
        this.userManager = userManager;
        threadCrypto = new Thread(EncryptFile);
    }

    public bool AddFile(FileViewModel fileView)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var uploadFolder = Path.Combine(currentDirectory, "UploadFiles");
        var uploadedDirectory = Path.Combine(uploadFolder, $"{httpContext.HttpContext?.User.Identity.Name}");
        if (!Directory.Exists(uploadedDirectory)) { Directory.CreateDirectory(uploadedDirectory); }
        var filepath = Path.Combine(uploadedDirectory, $"{fileView.Name}.{fileView.File.ContentType.Split('/')[1]}");
        var filepathCrypto = Path.Combine(uploadedDirectory, $"{fileView.Name}.encrypted");
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
                CryptoPath = filepathCrypto,
                Tag = fileView.Tag,
                IdUser = userManager.GetUserId(httpContext.HttpContext?.User),
                FileType = fileView.File.ContentType.Split('/')[1],
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(fileView.Password)
            };
            AddFileToDbAsync(file);
            threadCrypto.Start(new CryptoFileParamater()
            {
                InputFile = filepath,
                OutputFile = filepathCrypto,
                Password = fileView.Password
            });
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


    //File cryptoLogic
    public void EncryptFile(object paramater)
    {
        if (paramater is CryptoFileParamater)
        {
            var param = paramater as CryptoFileParamater;
            var password = param.Password;
            var inputFile = param.InputFile;
            var outputFile = param.OutputFile;
            // Генерируем ключ и вектор инициализации (IV) из пароля
            using var aes = Aes.Create();
            var key = GenerateKeyFromPassword(password, aes.KeySize / 8);
            var iv = GenerateKeyFromPassword(password, aes.BlockSize / 8);

            aes.Key = key;
            aes.IV = iv;

            // Шифруем файл
            using var fileStream = new FileStream(outputFile, FileMode.Create);
            using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using var inputStream = new FileStream(inputFile, FileMode.Open);

            inputStream.CopyTo(cryptoStream);
            DeleteFileByPath(inputFile);

        }
    }

    private byte[] GenerateKeyFromPassword(string password, int keySize)
    {
        using var sha256 = SHA256.Create();
        var keyBytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(keyBytes);

        // Триммируем до нужного размера
        var key = new byte[keySize];
        Array.Copy(hash, key, keySize);
        return key;
    }

    public void DecryptFile(string inputFile, string outputFile, string password)
    {
        // Генерируем ключ и вектор инициализации (IV) из пароля
        using var aes = Aes.Create();
        var key = GenerateKeyFromPassword(password, aes.KeySize / 8);
        var iv = GenerateKeyFromPassword(password, aes.BlockSize / 8);

        aes.Key = key;
        aes.IV = iv;

        // Расшифровываем файл
        using var fileStream = new FileStream(inputFile, FileMode.Open);
        using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var outputStream = new FileStream(outputFile, FileMode.Create);

        cryptoStream.CopyTo(outputStream);
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
        DeleteFileByPath(file.Path);
        if (file.CryptoPath != null) { DeleteFileByPath(file.CryptoPath); }
        return true;
    }
    public bool DeleteFileByPath(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            return false;
        }
        System.IO.File.Delete(path);
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
        DecryptFile(fileModel.CryptoPath, fileModel.Path, password);
        if (!System.IO.File.Exists(fileModel.Path))
        {
            file.State = StateExc.FileNotFound;
            return file;
        }
        file.BytesFile = System.IO.File.ReadAllBytes(fileModel.Path);
        threadCrypto.Start(new CryptoFileParamater()
        {
            InputFile = fileModel.Path,
            OutputFile = fileModel.CryptoPath,
            Password = password
        });
        file.FileName = $"{fileModel.Name}.{fileModel.FileType}";
        return file;
    }
}
