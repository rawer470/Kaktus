using System;
using Kaktus.Models;

namespace Kaktus.Services.Interfaces;

public interface IFileManagerService
{
    bool AddFile(FileViewModel fileView);
    bool DeleteFile(FileModel file);
    FileModel GetFileById(int id);
}
