using System;

namespace Kaktus.Models;
public class DownloadedFile
{
    public StateExc State { get; set; }
    public byte[] BytesFile { get; set; }
    public string FileName { get; set; }
    public bool IsPassword { get; set; }
}
