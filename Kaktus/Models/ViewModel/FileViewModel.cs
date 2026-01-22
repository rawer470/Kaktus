using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class FileViewModel
{
    [Required]
    public IFormFile File { get; set; } = null!;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Tag { get; set; } = string.Empty;
    public string? Password { get; set; }

}
