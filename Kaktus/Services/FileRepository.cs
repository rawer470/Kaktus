using System;
using Kaktus.Data;
using Kaktus.Models;
using Kaktus.Services.Interfaces;

namespace Kaktus.Services;

public class FileRepository : Repository<FileModel>, IFileRepository
{
    private readonly Context context;

    public FileRepository(Context context) : base(context)
    {
        this.context = context;
    }

}
