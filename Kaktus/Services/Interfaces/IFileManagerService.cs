using System;
using Kaktus.Models;

namespace Kaktus.Services.Interfaces;

public interface IFileManagerService
{
    bool AddFile(FileViewModel fileView);
    Task AddFileToDbAsync(FileModel fileModel);
    bool DeleteFile(string id);
    FileModel GetFileById(string id);
    DownloadedFile GetFileBytesById(string id);
    List<FileModel> GetAllUsersFiles();
}
