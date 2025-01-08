using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Models;
public enum StateExc
{
    OK,
    FileNotFound,
    WrongPassword
}
public class FileModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string CryptoPath { get; set; }
    public string Tag { get; set; }
    public string FileType { get; set; }
    public string PasswordHash { get; set; }
    [NotMapped]
    public StateExc state { get; set; }
    public string IdUser { get; set; }
    [ForeignKey("IdUser")]
    public User User { get; set; }
}
