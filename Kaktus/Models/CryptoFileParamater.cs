using System;

namespace Kaktus.Models;
/// <summary>
/// Нужен для того чтобы передать параметры в другой поток
/// </summary>
public class CryptoFileParamater
{
    public string InputFile { get; set; }
    public string OutputFile { get; set; }
    public string Password { get; set; }
}
