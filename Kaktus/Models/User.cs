using System;
using Microsoft.AspNetCore.Identity;

namespace Kaktus.Models;

public class User : IdentityUser
{
    public List<FileModel> Files { get; set; }
}
