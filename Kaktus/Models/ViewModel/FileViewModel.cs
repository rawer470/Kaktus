using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class FileViewModel
{
    public IFormFile File { get; set; }
    public string Name { get; set; }
    public string Tag { get; set; }
    [EmailAddress]
    public string EmailTo { get; set; }

    /// <summary>
    /// Проверка валидации модели
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (File != null && Name != null && Tag != null && EmailTo != null) { return true; }
        return false;
    }
}
