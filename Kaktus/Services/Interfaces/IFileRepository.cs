using System;
using Kaktus.Models;

namespace Kaktus.Services.Interfaces;

public interface IFileRepository : IRepository<FileModel>
{
    void IncludeUser();
    Task IncludeUserAsync();
    List<FileModel> GetFilesFromUser(string userId);
}
