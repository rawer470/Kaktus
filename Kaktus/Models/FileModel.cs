using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Kaktus.Models;

public class FileModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Tag { get; set; }
    public string Bag_Id { get; set; }
}
