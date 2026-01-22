using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class RegistrationModel
{
    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string RepeatPassword { get; set; } = string.Empty;
}
