using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace ContentServer.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly string _root;
    private readonly ILogger<FileController> _logger;
    private readonly Aes _aes;

    public FileController(
        ILogger<FileController> logger,
        Settings settings
        )
    {
        _root = settings.ContentDirectory;
        _logger = logger;
        
    }

    [HttpGet(Name = "{file}")]
    public void Get(string file, string? key = null)
    {
        var path = Path.Combine(_root, file);
        var actualFile = new FileInfo(path);

        if (!actualFile.Exists)
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return;
        }

        var length = actualFile.Length;
        Response.Clear();
        Response.Headers.ContentDisposition = $"attachment; filename={actualFile.Name};";
        Response.
    }

    private Stream GetFileStream(FileInfo file, byte[]? key = null, byte[]? salt = null)
    {
        var fs = file.OpenRead();
    }

    private Aes GetCrypto(byte[]? key = null, byte[]? salt = null, int iterations = 50000)
    {
        key ??= Array.Empty<byte>();
        salt ??= Array.Empty<byte>();
        
        
        var aes = Aes.Create();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Padding = PaddingMode.PKCS7;

        var rfcKey = new Rfc2898DeriveBytes(key, salt, iterations);

        aes.Key = rfcKey.GetBytes(aes.KeySize / 8);
        aes.IV = rfcKey.GetBytes(aes.BlockSize / 8);
        
        aes.Mode = CipherMode.CFB;
        
        aes.Key = 
    }
}