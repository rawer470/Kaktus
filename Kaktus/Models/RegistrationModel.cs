using System;
using System.ComponentModel.DataAnnotations;

namespace Kaktus.Models;

public class RegistrationModel
{
    [EmailAddress]
    [Required]
    public string EmailAddress { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string RepeatPassword { get; set; }
}
