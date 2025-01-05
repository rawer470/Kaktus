using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class FileViewModel
{
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Tag { get; set; }
    public string Password { get; set; }

}
