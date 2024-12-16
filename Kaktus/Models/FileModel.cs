using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Models;

public class FileModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Tag { get; set; }
    public string IdUser { get; set; }
    [ForeignKey("IdUser")]
    public User user { get; set; }
}
