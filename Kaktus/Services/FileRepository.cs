using System;
using Kaktus.Data;
using Kaktus.Models;
using Kaktus.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Services;

public class FileRepository : Repository<FileModel>, IFileRepository
{
    public Context context;

    public FileRepository(Context context) : base(context)
    {
        this.context = context;
    }

    public List<FileModel> GetFilesFromUser(string userId)
    {
        List<FileModel> files = context.Files.Where(x => x.IdUser == userId).ToList();
        return files;
    }

    public void IncludeUser()
    {
        context.Users.Include(x => x.Files);
    }
    public async Task IncludeUserAsync()
    {
        IncludeUser();
    }
}
