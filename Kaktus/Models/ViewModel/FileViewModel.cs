using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class FileViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Tag { get; set; }
    [EmailAddress]
    public string EmailTo { get; set; }

    /// <summary>
    /// Проверка валидации модели
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (Name != null && Tag != null && EmailTo != null) { return true; }
        return false;
    }
}
