using System;
using Kaktus.Models;

namespace Kaktus.Services.Interfaces;

public interface IFileManagerService
{
    bool AddFile(FileViewModel fileView);
    Task AddFileToDbAsync(FileModel fileModel);
    bool DeleteFile(string id);
    bool DeleteFileByPath(string path);
    FileModel GetFileById(string id);
    FileModel GetFileById(string id, string password);
    DownloadedFile GetFileBytesById(string id);
    DownloadedFile GetFileBytesById(string id, string password);
    List<FileModel> GetAllUsersFiles();
}
